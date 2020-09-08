using System;
using System.Collections.Generic;
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
            HeroDb.InitializeFromDisk();
            MatchDb.InitializeFromDisk();

            Console.WriteLine($"First Match: {MatchDb.Matches.First().Time}");
            Console.WriteLine($"Last Match: {MatchDb.Matches.Last().Time}");

            Logger.Log("Filtering matches with win/loss inconsistencies..");
            MatchDb.FilterMatches(match => match.CheckWinLossConsistency());

            PrintMapStats();

            Logger.Log("Filtering matches other than Midwars matches..");
            MatchDb.FilterMatches(match => match.Map == "midwars");

            PrintMatchTypeStats();

            Logger.Log("Filtering matches other than 2v2 and 3v2's..");
            MatchDb.FilterMatches(match => match.Type.In(MatchType.TwoVsTwo, MatchType.ThreeVsTwo));

            PrintPlayerStats();
            PrintHeroStats();
        }

        private static void PrintMapStats()
        {
            Console.WriteLine();
            Console.WriteLine();
            PrintTitle("Map Stats");
            foreach (var mapGroup in MatchDb.Matches
                .GroupBy(m => m.Map))
            {
                Console.WriteLine($"{mapGroup.First().Map}: {mapGroup.Count()} matches");
            }
        }

        private static void PrintMatchTypeStats()
        {
            Console.WriteLine();
            Console.WriteLine();
            PrintTitle("Match Stats");
            foreach (var matchType in Enum.GetValues(typeof(MatchType)).Cast<MatchType>())
            {
                Console.WriteLine($"{matchType}: {MatchDb.Matches.Count(m => m.Type == matchType)} matches");
            }
        }

        private static void PrintPlayerStats()
        {
            foreach (var matchGroupGroup in MatchDb.Matches
                .GroupBy(m => m.Type)
                .OrderBy(mg => mg.Key))
            {
                Console.WriteLine();
                Console.WriteLine();
                PrintTitle(matchGroupGroup.Key.ToString());
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

        private static void PrintHeroStats()
        {
            Console.WriteLine();
            Console.WriteLine();
            PrintTitle("Hero Stats");
            PrintHeroStatsImpl(MatchDb.Matches.SelectMany(m => m.PlayerResults), 1);

            foreach (var player in Honzor.Players)
            {
                Console.WriteLine();
                Console.WriteLine();
                PrintTitle($"{player.Nickname}'s Hero Stats");
                PrintHeroStatsImpl(MatchDb.Matches.SelectMany(m => m.PlayerResults)
                    .Where(r => r.Player.Nickname == player.Nickname), 5);
            }
        }

        private static void PrintHeroStatsImpl(IEnumerable<PlayerResult> results, int minPicks)
        {
            var heroStats = HeroDb.Heroes.ToDictionary(hero => hero.Id, hero => (Hero: hero, Picks: 0, Wins: 0));
            foreach (var result in results)
            {
                var heroStat = heroStats[result.Hero.Id];
                ++heroStat.Picks;
                if (result.Wins)
                    ++heroStat.Wins;
                heroStats[result.Hero.Id] = heroStat;
            }

            Console.WriteLine($"Total Hero Picks = {heroStats.Values.Sum(hero => hero.Picks)}");

            foreach (var (hero, picks, wins) in heroStats.Values
                //.OrderByDescending(heroStat => (double) heroStat.Wins / heroStat.Picks))
                .OrderByDescending(heroStat => heroStat.Picks)
                .Where(heroStat => heroStat.Picks >= minPicks))
            {
                Console.WriteLine($"{hero.Name}: {wins}/{picks} ({(double) wins / picks * 100:#.}%)");
            }
        }

        private static void PrintTitle(string title)
        {
            title = $"=={title}==";
            var underline = Enumerable.Repeat('=', title.Length).StringJoin();

            Console.WriteLine(title);
            Console.WriteLine(underline);
        }
    }
}