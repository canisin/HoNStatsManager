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
            var matchHistory = Honzor.GetMatchHistory();
            matchHistory = matchHistory.SkipWhile(m => m.Date < DateTime.Parse("2015-05-05")).ToMatchHistory();
            var matches = HonApi.GetMultiMatch(matchHistory).ToList();

            foreach (var matchType in Enum.GetValues(typeof(MatchType)).Cast<MatchType>())
            {
                Console.WriteLine($"{matchType}: {matches.Count(m => m.Type == matchType)}");
            }

            Console.WriteLine($"Match history: {matchHistory.Count}");
            Console.WriteLine($"Matches: {matches.Count}");

            Console.WriteLine($"HonApi rate limit wait count = {HonApi.WaitCount}");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"First match history id and date: {matchHistory.First().Id} - {matchHistory.First().Date}");
            Console.WriteLine($"Last match history id and date: {matchHistory.Last().Id} - {matchHistory.Last().Date}");
            Console.WriteLine();
            Console.WriteLine($"First match id and date: {matches.First().Id} - {matches.First().Date}");
            Console.WriteLine($"Last match id and date: {matches.Last().Id} - {matches.Last().Date}");
        }
    }
}