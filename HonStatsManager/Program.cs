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
            HeroDb.InitializeFromDisk();
            MatchDb.InitializeFromDisk();

            PrintMatchTypeStats();
            PrintPlayerStats();
            MatchDb.FilterMatches(match => match.Type.In(MatchType.TwoVsTwo, MatchType.ThreeVsTwo));
            PrintHeroStats();
        }

        private static void PrintMatchTypeStats()
        {
            Console.WriteLine();
            Console.WriteLine();
            PrintTitle("Match Counts");
            foreach (var matchType in Enum.GetValues(typeof(MatchType)).Cast<MatchType>())
            {
                Console.WriteLine($"{matchType}: {MatchDb.Matches.Count(m => m.Type == matchType)} matches");
            }
        }

        private static void PrintPlayerStats()
        {
            foreach (var matchGroupGroup in MatchDb.Matches
                .Where(m => m.Type != MatchType.Other)
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
            PrintTitle("Hero Stats (only 2v2 and 2v3 matches");

            var heroStats = HeroDb.Heroes.ToDictionary(hero => hero.Id, hero => (Hero: hero, Picks: 0, Wins: 0));
            foreach (var match in MatchDb.Matches)
            {
                foreach (var playerResult in match.PlayerResults)
                {
                    var heroStat = heroStats[playerResult.Hero.Id];
                    ++heroStat.Picks;
                    if (playerResult.Wins)
                        ++heroStat.Wins;
                    heroStats[playerResult.Hero.Id] = heroStat;
                }
            }

            foreach (var (hero, picks, wins) in heroStats.Values
                .OrderByDescending(heroStat => (double) heroStat.Wins / heroStat.Picks))
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