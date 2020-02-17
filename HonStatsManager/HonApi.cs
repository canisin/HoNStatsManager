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
        private const int MultiMatchBucketCount = 25;
        private const int RateLimitWait = 1000;

        public static List<(string Id, DateTime Date)> GetMatchHistory(Player player)
        {
            Logger.Log($"Getting match history for {player.Nickname}");

            var response = (JArray) Get($"match_history/public/accountid/{player.AccountId}");
            var history = ((string) response[0]["history"]).Split(',').Select(item => item.Split('|'));
            return history.Select(item => (item.First(), DateTime.ParseExact(item.Last(), "MM/dd/yyyy", null))).ToList();
        }

        public static IEnumerable<Match> GetMultiMatch(List<string> matchIds)
        {
            Logger.Log($"Querying {matchIds.Count} matches..");
            var queryCount = 0;
            var stopWatch = Stopwatch.StartNew();

            foreach (var bucket in matchIds.SplitBy(MultiMatchBucketCount))
            {
                var response = (JArray) Get($"multi_match/all/matchids/{string.Join("+", bucket)}");

                queryCount += bucket.Count;
                var currentDuration = stopWatch.Elapsed.TotalSeconds;
                var estimatedDuration = currentDuration / queryCount * matchIds.Count;
                Logger.Log($"{queryCount}/{matchIds.Count}" +
                           $" - Current duration: {TimeSpan.FromSeconds(currentDuration)}" +
                           $" - Estimated time to complete: {TimeSpan.FromSeconds(estimatedDuration)}");

                if (response == null)
                {
                    continue;
                }

                foreach (var matchId in bucket)
                {
                    var settings = response[0].SingleOrDefault(token => (string) token["match_id"] == matchId);
                    if (settings == null)
                    {
                        continue;
                    }

                    var inventories = response[1].Where(token => (string) token["match_id"] == matchId);
                    var statistics = response[2].Where(token => (string) token["match_id"] == matchId);
                    var summary = response[3].Single(token => (string) token["match_id"] == matchId);

                    yield return new Match(
                        matchId,
                        (DateTime) summary["mdt"],
                        TimeSpan.FromSeconds((int) summary["time_played"]),
                        statistics.Select(jPlayer => new PlayerResult(
                            new Player(
                                (string) jPlayer["account_id"],
                                (string) jPlayer["nickname"]),
                            ((int) jPlayer["team"]).ToTeam()
                        )).ToList());
                }
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