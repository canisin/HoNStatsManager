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
        public const string BaseUrl = @"http://api.heroesofnewerth.com";
        public const string Token = @"?token=0C0JQEHC8VZW5KFK";

        public static readonly DateTime StatsEpoch = DateTime.Parse("2015-05-05");

        public static readonly TimeZoneInfo TimeZone =
            TimeZoneInfo.CreateCustomTimeZone("hon", TimeSpan.FromHours(-5), "hon", "hon");

        private const int MultiMatchBucketCount = 25;
        private const int RateLimitWait = 1000;

        public static IEnumerable<MatchRecord> GetMatchHistory(Player player)
        {
            Logger.Log($"Getting match history for {player.Nickname}");

            var response = (JToken) Get($"match_history/public/accountid/{player.AccountId}");
            return MatchHistory.Parse(response)
                .Where(m => m.Date >= StatsEpoch);
        }

        public static IEnumerable<Match> GetMultiMatch(IEnumerable<MatchRecord> matchHistory)
        {
            matchHistory = matchHistory as List<MatchRecord> ?? matchHistory.ToList();

            Logger.Log($"Querying {matchHistory.Count()} matches..");
            var timer = new MultiMatchQueryTimer(matchHistory.Count());

            foreach (var bucket in matchHistory.SplitBy(MultiMatchBucketCount))
            {
                var response = (JToken) Get($"multi_match/all/matchids/{string.Join("+", bucket.Select(m => m.Id))}");

                timer.Update(bucket.Count);

                if (response == null)
                {
                    continue;
                }

                foreach (var matchRecord in bucket)
                {
                    if (!Match.CheckMatchId(response, matchRecord))
                    {
                        continue;
                    }

                    yield return new Match(response, matchRecord);
                }
            }

            Logger.Log($"Queried {matchHistory.Count()} matches in {timer.Timer.Elapsed}.");
            Logger.Log($"HonApi rate limit wait count = {WaitCount}");
        }

        private class MultiMatchQueryTimer
        {
            public Stopwatch Timer { get; }

            private readonly int _totalCount;
            private int _queryCount;

            public MultiMatchQueryTimer(int totalCount)
            {
                _totalCount = totalCount;
                _queryCount = 0;

                Timer = Stopwatch.StartNew();
            }

            public void Update(int updateCount)
            {
                _queryCount += updateCount;

                var currentDuration = Timer.Elapsed.TotalSeconds;
                var estimatedDuration = currentDuration / _queryCount * (_totalCount - _queryCount);
                Logger.Log($"{_queryCount}/{_totalCount}" +
                           $" - Current duration: {TimeSpan.FromSeconds(currentDuration)}" +
                           $" - Estimated time to complete: {TimeSpan.FromSeconds(estimatedDuration)}");
            }
        }

        public static Match GetMatch(MatchRecord matchRecord)
        {
            var response = (JToken) Get($"match/all/matchid/{matchRecord.Id}");

            if (response == null)
                return null;

            return new Match(response, matchRecord);
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

        private static int WaitCount { get; set; }

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