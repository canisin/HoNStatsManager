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
        public string Map { get; set; }
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
            Date = TimeZoneInfo.ConvertTimeToUtc((DateTime) summary["mdt"], HonApi.TimeZone);
            Duration = TimeSpan.FromSeconds((int) summary["time_played"]);
            PlayerResults = statistics.Select(t => new PlayerResult(t)).ToList();
            Map = (string) summary["map"];

            Type = this.GetMatchType();
        }

        public List<(string Key, int Size, bool IsWinner)> GetTeams()
        {
            return Enum.GetValues(typeof(Team)).Cast<Team>()
                .Select(t => PlayerResults.Where(pr => pr.Team == t).ToList())
                .Select(t => (
                    Key: t.Select(pr => pr.Player.Nickname)
                        .OrderBy(p => p)
                        .StringJoin(" - "),
                    Size: t.Count,
                    IsWinner: t.First().Wins))
                .OrderBy(t => t.Size)
                .ThenBy(t => t.Key)
                .ToList();
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