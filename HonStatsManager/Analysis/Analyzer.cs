using System;
using System.Collections.Generic;
using System.Linq;
using HonStatsManager.Data;
using HonStatsManager.Utility;

namespace HonStatsManager.Analysis
{
    internal class Analyzer
    {
        private List<Match> _matches;

        public Analyzer()
        {
            _matches = MatchDb.Matches.ToList();
        }

        private void ApplyFilters()
        {
            var filters = Program.MainForm.GetFilters();

            if (filters == FilterType.None)
            {
                Logger.Log("No filters selected.");
                return;
            }

            var filteredMatchTypes = filters.GetFilteredMatchTypes();
            if (filteredMatchTypes.Any())
            {
                Logger.Log("Filtering match types..");
                FilterMatches(m => !m.Type.In(filteredMatchTypes));
            }

            var filteredMaps = filters.GetFilteredMaps();
            if (filteredMaps.Any())
            {
                Logger.Log("Filtering maps..");
                FilterMatches(m => !m.Map.In(filteredMaps));
            }

            if (filters.HasFlag(FilterType.DataKicks))
            {
                Logger.Log("Filtering matches with kicked players..");
                FilterMatches(m => !m.PlayerResults.Any(r => r.Kicked));
            }

            if (filters.HasFlag(FilterType.DataDiscos))
            {
                Logger.Log("Filtering matches with disconnected players..");
                FilterMatches(m => !m.PlayerResults.Any(r => r.Discos));
            }

            if (filters.HasFlag(FilterType.DataIncomplete))
            {
                Logger.Log("Filtering incomplete matches..");
                FilterMatches(m => m.PlayerResults.Any(r => r.Wins));
            }

            if (filters.HasFlag(FilterType.DataMissingHeroes))
            {
                Logger.Log("Filtering matches with missing hero information..");
                FilterMatches(m => m.PlayerResults.All(r => r.HeroId.KeyIn(HeroDb.HeroDict)));
            }
        }

        private void FilterMatches(Func<Match, bool> predicate)
        {
            var initialCount = _matches.Count;
            _matches = _matches.Where(predicate).ToList();
            Logger.Log($"{initialCount - _matches.Count} matches filtered, {_matches.Count} matches remain.");
        }

        public void PrintMapStats()
        {
            ApplyFilters();
            Logger.Log();

            PrintTitle("Map Stats");
            foreach (var mapGroup in _matches
                .GroupBy(m => m.Map))
            {
                Logger.Log($"{mapGroup.Key}: {mapGroup.Count()} matches");
            }
        }

        public void PrintMatchTypeStats()
        {
            ApplyFilters();
            Logger.Log();

            PrintTitle("Match Stats");
            foreach (var matchType in Enum.GetValues(typeof(MatchType)).Cast<MatchType>())
            {
                Logger.Log($"{matchType}: {_matches.Count(m => m.Type == matchType)} matches");
            }
        }

        public void PrintPlayerStats()
        {
            ApplyFilters();
            Logger.Log();

            foreach (var matchGroupGroup in _matches
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

        public void PrintAllHeroStats()
        {
            ApplyFilters();
            Logger.Log();

            PrintTitle("Hero Stats");
            PrintHeroStatsImpl(_matches.SelectMany(m => m.PlayerResults));
        }

        public void PrintPlayerHeroStats(Player player)
        {
            ApplyFilters();
            Logger.Log();

            const int minPicks = 5;
            PrintTitle($"{player.Nickname}'s Hero Stats");
            Logger.Log($"(Only heroes with at least {minPicks} picks are shown.)");
            PrintHeroStatsImpl(_matches.SelectMany(m => m.PlayerResults)
                .Where(r => r.Player.Nickname == player.Nickname), minPicks);
        }

        private static void PrintHeroStatsImpl(IEnumerable<PlayerResult> results, int minPicks = 0)
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
                var ratio = picks > 0 ? (double) wins / picks : 0.0;
                Logger.Log($"{hero.Name, -20}: {wins,2}/{picks,-2} ({ratio:P0})");
            }
        }

        public void PrintGameNightStats()
        {
            ApplyFilters();
            Logger.Log();

            PrintTitle("Game Night Stats");
            foreach (var yearGroup in _matches
                .GroupBy(m => m.GameNight)
                .GroupBy(nightGroup => (nightGroup.Key.Year, nightGroup.Key.Month))
                .GroupBy(monthGroup => monthGroup.Key.Year))
            {
                var yearTitle =
                    $"{yearGroup.Key} ({yearGroup.Sum(monthGroup => monthGroup.Sum(nightGroup => nightGroup.Count()))} matches):";
                Logger.Log(yearTitle);
                Logger.Log(Enumerable.Repeat('-', yearTitle.Length).StringJoin());
                foreach (var monthGroup in yearGroup)
                {
                    Logger.Log(
                        $"{monthGroup.First().Key:MMMM yyyy} ({monthGroup.Sum(nightGroup => nightGroup.Count())} matches):");
                    foreach (var nightGroup in monthGroup)
                        Logger.Log($" {nightGroup.Key,-25:D}: {nightGroup.Count(),2} matches");

                    Logger.Log();
                }

                Logger.Log();
            }
        }

        public void PrintGameMonthStats()
        {
            ApplyFilters();
            Logger.Log();

            PrintTitle("Game Night Monthly Stats");
            foreach (var yearGroup in _matches
                .GroupBy(m => (m.GameNight.Year, m.GameNight.Month))
                .GroupBy(monthGroup => monthGroup.Key.Year))
            {
                Logger.Log($"{yearGroup.Key} ({yearGroup.Sum(monthGroup => monthGroup.Count())} matches):");
                foreach (var monthGroup in yearGroup)
                    Logger.Log($" {monthGroup.First().GameNight,-12:MMMM yyyy}: {monthGroup.Count(),3} matches");

                Logger.Log();
            }
        }

        public void PrintGameYearStats()
        {
            ApplyFilters();
            Logger.Log();

            PrintTitle("Game Night Yearly Stats");
            foreach (var yearGroup in _matches
                .GroupBy(m => m.GameNight.Year))
            {
                Logger.Log($"{yearGroup.Key}: {yearGroup.Count(),4} matches");
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