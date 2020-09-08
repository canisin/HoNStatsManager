using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace HonStatsManager
{
    internal static class Honzor
    {
        public static readonly TimeZoneInfo TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");

        public static DateTime CalcGameNight(this Match match)
        {
            var honzorTime = TimeZoneInfo.ConvertTimeFromUtc(match.Time, TimeZone);
            var gameNight = honzorTime.Date;

            if (honzorTime.TimeOfDay <= TimeSpan.Parse("06:00"))
                gameNight = gameNight.AddDays(-1);

            return gameNight;
        }

        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public static List<Player> Players
            = new List<Player>
            {
                new Player("1220342", "budrick"),
                new Player("1248746", "IfUCanUCan"),
                new Player("2997424", "sbarcan"),
                new Player("1260616", "papercute"),
                new Player("1248693", "Turritopsis")
            };

        public static bool IsMember(Player player)
        {
            return player.AccountId.In(Players.Select(p => p.AccountId));
        }

        public static IEnumerable<MatchRecord> GetMatchHistory()
        {
            return Players
                .SelectMany(HonApi.GetMatchHistory)
                .Distinct()
                .OrderBy(m => m.Date);
        }
    }
}