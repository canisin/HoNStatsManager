using System;
using System.Collections.Generic;

namespace HonStatsManager
{
    internal class Match
    {
        public string Id { get; }
        public DateTime Date { get; }
        public TimeSpan Duration { get; }
        public List<PlayerResult> PlayerResults { get; }
        public MatchType Type { get; }

        public Match(string id, DateTime date, TimeSpan duration, List<PlayerResult> playerResults)
        {
            Id = id;
            Date = date;
            Duration = duration;
            PlayerResults = playerResults;

            Type = this.GetMatchType();
        }
    }
}