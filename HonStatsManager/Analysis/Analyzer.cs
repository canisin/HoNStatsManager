using System;
using System.Collections.Generic;
using System.Linq;
using HonStatsManager.Data;
using HonStatsManager.Utility;

namespace HonStatsManager.Analysis
{
    internal static class Analyzer
    {
        private static void ApplyFilters()
        {
            var filters = Program.MainForm.GetFilters();

            MatchDb.FilterMatches(m => !m.Type.In(filters.GetFilteredMatchTypes()));

            MatchDb.FilterMatches(m => !m.Map.In(filters.GetFilteredMaps()));

            if (filters.HasFlag(FilterType.DataKicks))
                MatchDb.FilterMatches(m => !m.PlayerResults.Any(r => r.Kicked));

            if (filters.HasFlag(FilterType.DataDiscos))
                MatchDb.FilterMatches(m => !m.PlayerResults.Any(r => r.Discos));

            if (filters.HasFlag(FilterType.DataIncomplete))
                MatchDb.FilterMatches(m => !m.PlayerResults.Any(r => r.Wins));

            if (filters.HasFlag(FilterType.DataMissingHeroes))
                MatchDb.FilterMatches(m => m.PlayerResults.All(r => r.HeroId.KeyIn(HeroDb.HeroDict)));
        }

        public static void PrintMapStats()
        {
            Logger.Log();
            Logger.Log();
            MatchDb.Reload();
            ApplyFilters();
            Logger.Log();

            PrintTitle("Map Stats");
            foreach (var mapGroup in MatchDb.Matches
                .GroupBy(m => m.Map))
            {
                Logger.Log($"{mapGroup.First().Map}: {mapGroup.Count()} matches");
            }
        }

        public static void PrintMatchTypeStats()
        {
            Logger.Log();
            Logger.Log();
            MatchDb.Reload();
            ApplyFilters();
            Logger.Log();

            PrintTitle("Match Stats");
            foreach (var matchType in Enum.GetValues(typeof(MatchType)).Cast<MatchType>())
            {
                Logger.Log($"{matchType}: {MatchDb.Matches.Count(m => m.Type == matchType)} matches");
            }
        }

        public static void PrintPlayerStats()
        {
            Logger.Log();
            Logger.Log();
            MatchDb.Reload();
            ApplyFilters();
            Logger.Log();

            foreach (var matchGroupGroup in MatchDb.Matches
                .GroupBy(m => m.Type)
                .OrderBy(mg => mg.Key))
            {
                Logger.Log();
                Logger.Log();
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

                    Logger.Log();
                    Logger.Log($"{matchGroup.Key} -> {totalMatches} matches");
                    Logger.Log($"{team1}: {team1Wins} wins ({team1Ratio:P})");
                    Logger.Log($"{team2}: {team2Wins} wins ({team2Ratio:P})");
                }
            }
        }

        public static void PrintHeroStats()
        {
            Logger.Log();
            Logger.Log();
            MatchDb.Reload();
            ApplyFilters();
            Logger.Log();

            PrintTitle("Hero Stats");
            PrintHeroStatsImpl(MatchDb.Matches.SelectMany(m => m.PlayerResults), 1);

            foreach (var player in Honzor.Players)
            {
                Logger.Log();
                Logger.Log();
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
                var heroStat = heroStats[result.HeroId];
                ++heroStat.Picks;
                if (result.Wins)
                    ++heroStat.Wins;
                heroStats[result.HeroId] = heroStat;
            }

            Logger.Log($"Total Hero Picks = {heroStats.Values.Sum(hero => hero.Picks)}");

            foreach (var (hero, picks, wins) in heroStats.Values
                //.OrderByDescending(heroStat => (double) heroStat.Wins / heroStat.Picks))
                .OrderByDescending(heroStat => heroStat.Picks)
                .Where(heroStat => heroStat.Picks >= minPicks))
            {
                Logger.Log($"{hero.Name}: {wins}/{picks} ({(double) wins / picks * 100:#.}%)");
            }
        }

        private static void PrintTitle(string title)
        {
            title = $"=={title}==";
            var underline = Enumerable.Repeat('=', title.Length).StringJoin();

            Logger.Log(title);
            Logger.Log(underline);
        }
    }
}