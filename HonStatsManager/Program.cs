using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

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
            //Download();
            var matches = Read();

            Console.WriteLine();
            Console.WriteLine();
            foreach (var matchType in Enum.GetValues(typeof(MatchType)).Cast<MatchType>())
            {
                Console.WriteLine($"{matchType}: {matches.Count(m => m.Type == matchType)}");
            }

            Console.WriteLine();
            foreach (var matchGroupGroup in matches
                .Where(m => m.Type != MatchType.Other)
                .GroupBy(m => Enum.GetValues(typeof(Team)).Cast<Team>()
                    .Select(t => m.PlayerResults
                        .Where(pr => pr.Team == t)
                        .Select(pr => pr.Player.Nickname)
                        .OrderBy(p => p)
                        .ToList())
                    .Select(t => (Count: t.Count, Key: t.StringJoin(" - ")))
                    .OrderBy(t => t.Count)
                    .ThenBy(t => t.Key)
                    .Select(t => t.Key)
                    .StringJoin(" vs "))
                .OrderBy(mg => mg.Key)
                .GroupBy(mg => mg.First().Type)
                .OrderBy(mgg => mgg.Key))
            {
                Console.WriteLine();
                Console.WriteLine(matchGroupGroup.Key);
                foreach (var matchGroup in matchGroupGroup)
                {
                    Console.WriteLine($"{matchGroup.Key} -> {matchGroup.Count()} matches");
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            foreach (var match in matches)
            {
                foreach (var playerResult in match.PlayerResults)
                {
                    if (playerResult.Wins > 1)
                    {
                        Console.WriteLine(
                            $"matchid: {match.Id}, nickname: {playerResult.Player.Nickname}, wins: {playerResult.Wins}");
                    }

                    if (playerResult.Losses > 1)
                    {
                        Console.WriteLine(
                            $"matchid: {match.Id}, nickname: {playerResult.Player.Nickname}, losses: {playerResult.Losses}");
                    }

                    if (playerResult.Concedes > 1)
                    {
                        Console.WriteLine(
                            $"matchid: {match.Id}, nickname: {playerResult.Player.Nickname}, concedes: {playerResult.Concedes}");
                    }

                    if (playerResult.Discos > 1)
                    {
                        Console.WriteLine(
                            $"matchid: {match.Id}, nickname: {playerResult.Player.Nickname}, discos: {playerResult.Discos}");
                    }

                    if (playerResult.Kicked > 1)
                    {
                        Console.WriteLine(
                            $"matchid: {match.Id}, nickname: {playerResult.Player.Nickname}, kicked: {playerResult.Kicked}");
                    }

                    if (playerResult.Wins + playerResult.Losses + playerResult.Concedes + playerResult.Discos +
                        playerResult.Kicked != 1)
                    {
                        Console.WriteLine(
                            $"matchid: {match.Id}, nickname: {playerResult.Player.Nickname}, " +
                            $" wins: {playerResult.Wins}" +
                            $" losses: {playerResult.Losses}" +
                            $" concedes: {playerResult.Concedes}" +
                            $" discos: {playerResult.Discos}" +
                            $" kicked: {playerResult.Kicked}");
                    }

                    if (playerResult.Wins + playerResult.Losses != 1)
                    {
                        Console.WriteLine(
                            $"matchid: {match.Id}, nickname: {playerResult.Player.Nickname}, " +
                            $" wins: {playerResult.Wins}" +
                            $" losses: {playerResult.Losses}");
                    }
                }
            }
        }

        private const string FileName = @"matches.db";

        private static void Download()
        {
            var matchHistory = Honzor.GetMatchHistory();
            Console.WriteLine($"Total match history: {matchHistory.Count}");

            matchHistory = matchHistory.SkipWhile(m => m.Date < DateTime.Parse("2015-05-05")).ToMatchHistory();
            Console.WriteLine($"Match history after 2015-05-05: {matchHistory.Count}");

            var matches = HonApi.GetMultiMatch(matchHistory).ToList();
            Console.WriteLine($"Downloaded matches: {matches.Count}");
            Console.WriteLine($"HonApi rate limit wait count = {HonApi.WaitCount}");

            File.WriteAllText(FileName, JsonConvert.SerializeObject(matches));
        }

        private static List<Match> Read()
        {
            var matches = JsonConvert.DeserializeObject<List<Match>>(File.ReadAllText(FileName));
            Console.WriteLine($"Matches read from file: {matches.Count}");
            Console.WriteLine($"Last match id and date: {matches.Last().Id} - {matches.Last().Date}");
            return matches;
        }
    }
}