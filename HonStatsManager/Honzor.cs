using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace HonStatsManager
{
    internal static class Honzor
    {
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