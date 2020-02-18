using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace HonStatsManager
{
    internal class Match
    {
        public string Id { get; }
        public DateTime Date { get; }
        public TimeSpan Duration { get; }
        public List<PlayerResult> PlayerResults { get; }
        public MatchType Type { get; }

        public Match(string id, JArray apiResponse)
        {
            var settings = apiResponse[0].Single(id.CheckMatchId);
            var inventories = apiResponse[1].Where(id.CheckMatchId);
            var statistics = apiResponse[2].Where(id.CheckMatchId);
            var summary = apiResponse[3].Single(id.CheckMatchId);

            Id = id;
            Date = (DateTime) summary["mdt"];
            Duration = TimeSpan.FromSeconds((int) summary["time_played"]);
            PlayerResults = statistics.Select(jPlayer => new PlayerResult(
                new Player(
                    (string) jPlayer["account_id"],
                    (string) jPlayer["nickname"]),
                ((int) jPlayer["team"]).ToTeam()
            )).ToList();

            Type = this.GetMatchType();
        }
    }

    internal static class MatchExtensions
    {
        public static bool CheckMatchId(this string matchId, JToken token)
        {
            return (string) token["match_id"] == matchId;
        }
    }
}