﻿using System;
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
            var matchIds = Honzor.GetMatchHistory();
            var matches = HonApi.GetMultiMatch(matchIds).ToList();

            foreach (var matchType in Enum.GetValues(typeof(MatchType)).Cast<MatchType>())
            {
                Console.WriteLine($"{matchType}: {matches.Count(m => m.Type == matchType)}");
            }

            Console.WriteLine($"Match ids: {matchIds.Count}");
            Console.WriteLine($"Matches: {matches.Count}");

            Console.WriteLine($"HonApi rate limit wait count = {HonApi.WaitCount}");
        }
    }
}