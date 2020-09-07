using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HonStatsManager
{
    internal class Match
    {
        public string Id { get; }
        public DateTime Date { get; }
        public TimeSpan Duration { get; }
        public List<PlayerResult> PlayerResults { get; }
        public string Map { get; }
        public MatchType Type { get; }

        [JsonConstructor]
        public Match(string id, DateTime date, TimeSpan duration,
            List<PlayerResult> playerResults,
            string map, MatchType type)
        {
            Id = id;
            Date = date;
            Duration = duration;
            PlayerResults = playerResults;
            Map = map;
            Type = type;
        }

        public Match(JToken token, MatchRecord matchRecord)
        {
            var settings = token[0].Single(t => CheckMatchId(t, matchRecord.Id));
            var inventories = token[1].Where(t => CheckMatchId(t, matchRecord.Id));
            var statistics = token[2].Where(t => CheckMatchId(t, matchRecord.Id));
            var summary = token[3].Single(t => CheckMatchId(t, matchRecord.Id));

            Id = matchRecord.Id;
            Date = TimeZoneInfo.ConvertTimeToUtc((DateTime) summary["mdt"], HonApi.TimeZone);
            Duration = TimeSpan.FromSeconds((int) summary["time_played"]);
            PlayerResults = statistics.Select(t => new PlayerResult(t)).ToList();
            Map = (string) summary["map"];

            Type = this.GetMatchType();
        }

        public bool CheckWinLossConsistency()
        {
            var first = PlayerResults.First();
            var isConsistent = PlayerResults.Skip(1)
                .All(r => (r.Team == first.Team && r.Wins == first.Wins && r.Losses == first.Losses)
                          || (r.Team != first.Team && r.Wins != first.Wins && r.Losses != first.Losses));

            if (!isConsistent)
                Logger.Log($"Inconsistent wins and losses detected in match {Id}.");

            return isConsistent;
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

        public static bool CheckMatchId(JToken token, MatchRecord matchRecord)
        {
            return token[0].Any(t => CheckMatchId(t, matchRecord.Id));
        }

        private static bool CheckMatchId(JToken token, string matchId)
        {
            return (string) token["match_id"] == matchId;
        }
    }
}