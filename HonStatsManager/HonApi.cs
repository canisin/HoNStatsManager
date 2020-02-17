using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

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
            var history = (string) ((IEnumerable<dynamic>) response).First().history;
            return history.Split(',').Select(item => item.Split('|').First()).ToList();
        }

        public static dynamic GetMatch(string matchId)
        {
            Logger.Log($"Getting match {matchId}");
            return Get($"match/all/matchid/{matchId}");
        }

        private static dynamic Get(string parameters)
        {
            return JsonConvert.DeserializeObject<dynamic>(GetRaw(parameters));
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
    }
}