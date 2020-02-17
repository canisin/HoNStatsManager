using System;
using System.Collections.Generic;

namespace HonStatsManager
{
    internal class Match
    {
        public string MatchId { get; }
        public DateTime DateTime { get; }
        public TimeSpan Duration { get; }
        public List<PlayerResult> PlayerResults { get; }

        public Match(string matchId, DateTime dateTime, TimeSpan duration, List<PlayerResult> playerResults)
        {
            MatchId = matchId;
            DateTime = dateTime;
            Duration = duration;
            PlayerResults = playerResults;
        }
    }
}