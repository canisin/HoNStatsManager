using System;
using System.Linq;

namespace HonStatsManager
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            MainImpl(args);
            Console.WriteLine();
            Console.WriteLine("Please press any key to continue...");
            Console.ReadKey(true);
        }

        private static void MainImpl(string[] args)
        {
            var matchDb = MatchDb.FromUpdate();
            var matches = matchDb.Matches;

            Console.WriteLine();
            Console.WriteLine();
            foreach (var matchType in Enum.GetValues(typeof(MatchType)).Cast<MatchType>())
            {
                Console.WriteLine($"{matchType}: {matches.Count(m => m.Type == matchType)} matches");
            }

            Console.WriteLine();
            foreach (var matchGroupGroup in matches
                .Where(m => m.Type != MatchType.Other)
                .GroupBy(m => m.Type)
                .OrderBy(mg => mg.Key))
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(matchGroupGroup.Key);
                foreach (var matchGroup in matchGroupGroup
                    .Select(m => m.GetTeams())
                    .GroupBy(ts => ts.Select(t => t.Key).StringJoin(" vs "))
                    .OrderBy(mg => mg.Key))
                {
                    var team1 = matchGroup.First().First().Key;
                    var team2 = matchGroup.First().Last().Key;
                    var team1Wins = matchGroup.Count(mg => mg.First().IsWinner);
                    var team2Wins = matchGroup.Count(mg => mg.Last().IsWinner);
                    var totalMatches = matchGroup.Count();
                    var team1Ratio = (double) team1Wins / totalMatches;
                    var team2Ratio = (double) team2Wins / totalMatches;

                    Console.WriteLine();
                    Console.WriteLine($"{matchGroup.Key} -> {totalMatches} matches");
                    Console.WriteLine($"{team1}: {team1Wins} wins ({team1Ratio:P})");
                    Console.WriteLine($"{team2}: {team2Wins} wins ({team2Ratio:P})");
                }
            }
        }
    }
}