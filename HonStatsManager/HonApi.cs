using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HonStatsManager
{
    internal static class HonApi
    {
        public static readonly string BaseUrl = @"http://api.heroesofnewerth.com";
        public static readonly string Token = @"?token=0C0JQEHC8VZW5KFK";

        public static readonly DateTime StatsEpoch = DateTime.Parse("2015-05-05");

        private const int MultiMatchBucketCount = 25;
        private const int RateLimitWait = 1000;

        public static MatchHistory GetMatchHistory(Player player)
        {
            Logger.Log($"Getting match history for {player.Nickname}");

            var response = (JArray) Get($"match_history/public/accountid/{player.AccountId}");
            return new MatchHistory(response)
                .SkipWhile(m => m.Date < StatsEpoch)
                .ToMatchHistory();
        }

        public static IEnumerable<Match> GetMultiMatch(MatchHistory matchHistory)
        {
            Logger.Log($"Querying {matchHistory.Count} matches..");
            var timer = new MultiMatchQueryTimer(matchHistory.Count);

            foreach (var bucket in matchHistory.SplitBy(MultiMatchBucketCount))
            {
                var response = (JArray) Get($"multi_match/all/matchids/{string.Join("+", bucket.Select(m => m.Id))}");

                timer.Update(bucket.Count);

                if (response == null)
                {
                    continue;
                }

                foreach (var (matchId, _) in bucket)
                {
                    if (!response.First().Any(matchId.CheckMatchId))
                    {
                        continue;
                    }

                    yield return new Match(matchId, response);
                }
            }
        }

        private class MultiMatchQueryTimer
        {
            private readonly Stopwatch _timer;
            private readonly int _totalCount;
            private int _queryCount;

            public MultiMatchQueryTimer(int totalCount)
            {
                _totalCount = totalCount;
                _queryCount = 0;

                _timer = Stopwatch.StartNew();
            }

            public void Update(int bucketCount)
            {
                _queryCount += bucketCount;

                var currentDuration = _timer.Elapsed.TotalSeconds;
                var estimatedDuration = currentDuration / _queryCount * _totalCount;
                Logger.Log($"{_queryCount}/{_totalCount}" +
                           $" - Current duration: {TimeSpan.FromSeconds(currentDuration)}" +
                           $" - Estimated time to complete: {TimeSpan.FromSeconds(estimatedDuration)}");
            }
        }

        public static string GetMatchRaw(string matchId)
        {
            Logger.Log($"Getting match raw {matchId}");
            return GetRaw($"match/all/matchid/{matchId}");
        }

        private static object Get(string parameters)
        {
            return JsonConvert.DeserializeObject(GetRaw(parameters));
        }

        public static int WaitCount { get; private set; }

        private static string GetRaw(string parameters)
        {
            while (true)
            {
                using (var client = new WebClient())
                {
                    var url = string.Join("/", BaseUrl, parameters, Token);
                    Logger.Log($"Getting url {url}");
                    try
                    {
                        return client.DownloadString(url);
                    }
                    catch (WebException exception)
                    {
                        var code = (int) ((HttpWebResponse) exception.Response).StatusCode;
                        var description = ((HttpWebResponse) exception.Response).StatusDescription;

                        var responseStream = exception.Response.GetResponseStream();
                        var body = responseStream != null ? new StreamReader(responseStream).ReadToEnd() : "";

                        Logger.Log($"{code} - {description} - {body}");

                        switch (body)
                        {
                            case "No results.":
                                return "";

                            case "Too many requests":
                                Logger.Log("Waiting for rate limiter..");
                                ++WaitCount;
                                Thread.Sleep(RateLimitWait);
                                continue;

                            default:
                                throw;
                        }
                    }
                }
            }
        }
    }
}