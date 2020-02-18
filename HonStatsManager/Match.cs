using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace HonStatsManager
{
    internal class Match
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public List<PlayerResult> PlayerResults { get; set; }
        public MatchType Type { get; set; }

        public Match()
        {
        }

        public Match(string id, JToken token)
        {
            var settings = token[0].Single(id.CheckMatchId);
            var inventories = token[1].Where(id.CheckMatchId);
            var statistics = token[2].Where(id.CheckMatchId);
            var summary = token[3].Single(id.CheckMatchId);

            Id = id;
            Date = (DateTime) summary["mdt"];
            Duration = TimeSpan.FromSeconds((int) summary["time_played"]);
            PlayerResults = statistics.Select(t => new PlayerResult(t)).ToList();

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