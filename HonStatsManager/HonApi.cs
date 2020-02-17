using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HonStatsManager
{
    internal static class HonApi
    {
        public static readonly string BaseUrl = @"http://api.heroesofnewerth.com";
        public static readonly string Token = @"?token=0C0JQEHC8VZW5KFK";

        public static List<string> GetMatchHistory(Player player)
        {
            Logger.Log($"Getting match history for {player.Nickname}");

            var response = Get($"match_history/public/accountid/{player.AccountId}");
            var history = (string) ((JArray) response)[0]["history"];
            return history.Split(',').Select(item => item.Split('|').First()).ToList();
        }

        public static Match GetMatch(string matchId)
        {
            Logger.Log($"Getting match {matchId}");

            var response = Get($"match/all/matchid/{matchId}");
            var jMatch = new JMatch((JArray) response);

            return new Match(
                matchId,
                DateTime.Parse((string) jMatch.Summary["mdt"]),
                TimeSpan.FromSeconds((int) jMatch.Summary["time_played"]),
                jMatch.Statistics.Select(jPlayer => new PlayerResult(
                    new Player(
                        (string) jPlayer["account_id"],
                        (string) jPlayer["nickname"]),
                    ((int) jPlayer["team"]).ToTeam()
                )).ToList());
        }

        public static IEnumerable<Match> GetMultiMatch(IEnumerable<string> matchIds)
        {
            const int multiMatchBucketCount = 25;
            foreach (var bucket in matchIds.SplitBy(multiMatchBucketCount))
            {
                var response = (JArray) Get($"multi_match/all/matchids/{string.Join("+", bucket)}");
                foreach (var matchId in bucket)
                {
                    var settings = response[0].Single(foo => (string) foo["match_id"] == matchId);
                    var inventories = response[1].Where(foo => (string) foo["match_id"] == matchId);
                    var statistics = response[2].Where(foo => (string) foo["match_id"] == matchId);
                    var summary = response[3].Single(foo => (string) foo["match_id"] == matchId);

                    yield return new Match(
                        matchId,
                        DateTime.Parse((string) summary["mdt"]),
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

                        if (body == "No results.")
                        {
                            return "";
                        }

                        //if (body == "<rate limit error>")
                        //{
                        //    Thread.Sleep(1000);
                        //    continue;
                        //}

                        throw;
                    }
                }
            }
        }

        private class JMatch
        {
            public JObject Settings { get; }
            public JArray Inventories { get; }
            public JArray Statistics { get; }
            public JObject Summary { get; }

            public JMatch(JArray json)
            {
                Settings = (JObject) json[0][0];
                Inventories = (JArray) json[1];
                Statistics = (JArray) json[2];
                Summary = (JObject) json[3][0];
            }
        }
    }
}