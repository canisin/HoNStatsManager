using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HonStatsManager
{
    internal static class Honzor
    {
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public static List<Player> Players
            = new List<Player>
            {
                new Player("1220342", "budrick", "Can"),
                new Player("1248746", "IfUCanUCan", "Baran"),
                new Player("2997424", "sbarcan", "Barış"),
                new Player("1260616", "papercute", "Batu"),
                new Player("1248693", "Turritopsis", "Gökhan")
            };
    }
}