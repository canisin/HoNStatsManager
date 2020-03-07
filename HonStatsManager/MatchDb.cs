using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace HonStatsManager
{
    internal class MatchDb
    {
        public const string FileName = @"matches.db";

        public IReadOnlyList<Match> Matches => _matches.AsReadOnly();

        private List<Match> _matches;

        public void Read()
        {
            _matches = JsonConvert.DeserializeObject<List<Match>>(File.ReadAllText(FileName));
            Console.WriteLine($"Matches read from file: {_matches.Count}");
            Console.WriteLine($"Last match id and date: {_matches.Last().Id} - {_matches.Last().Date}");
        }

        public void Download()
        {
            var matchHistory = Honzor.GetMatchHistory();
            Console.WriteLine($"Total match history: {matchHistory.Count}");

            _matches = HonApi.GetMultiMatch(matchHistory).ToList();
            Console.WriteLine($"Downloaded matches: {_matches.Count}");
            Console.WriteLine($"HonApi rate limit wait count = {HonApi.WaitCount}");

            File.WriteAllText(FileName, JsonConvert.SerializeObject(_matches));
        }
    }
}