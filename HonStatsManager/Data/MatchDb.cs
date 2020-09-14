using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HonStatsManager.Utility;
using Newtonsoft.Json;

namespace HonStatsManager.Data
{
    internal static class MatchDb
    {
        public const string FileName = @"matches.db";

        public static IReadOnlyList<Match> Matches => _matches.AsReadOnly();

        private static List<Match> _matches;

        public static void Initialize()
        {
            Read();
        }

        public static void Update()
        {
            Download();
            Write();
        }

        public static void Reset()
        {
            Clear();
            Download();
            Write();
        }

        private static void Clear()
        {
            _matches.Clear();
        }

        private static void Read()
        {
            if (!File.Exists(FileName))
            {
                Logger.Log($"{FileName} not found.");
                return;
            }

            _matches = JsonConvert.DeserializeObject<List<Match>>(File.ReadAllText(FileName));

            Logger.Log($"Matches read from file: {_matches.Count}");
            Logger.Log($"Last match: {_matches.Last().Id} - {_matches.Last().Time.ToLocalTime()}");
        }

        private static void Download()
        {
            var newMatchRecords = Honzor.GetMatchHistory()
                .Where(mr => !mr.Id.In(_matches.Select(m => m.Id)))
                .ToList();

            if (!newMatchRecords.Any())
            {
                Logger.Log(_matches.Any()
                    ? "No new matches."
                    : "No matches found.");

                return;
            }

            Logger.Log($"Found {newMatchRecords.Count} {(_matches.Any() ? "new" : "")} matches.");

            var matches = HonApi.GetMultiMatch(newMatchRecords).OrderBy(m => m.Time).ToList();

            if (!matches.Any())
            {
                Logger.Log("Failed to download matches.");
                return;
            }

            Logger.Log($"Matches downloaded: {matches.Count}");
            Logger.Log($"Last match: {matches.Last().Id} - {matches.Last().Time.ToLocalTime()}");

            _matches.AddRange(matches);
            _matches = _matches.OrderBy(m => m.Time).ToList();
        }

        private static void Write()
        {
            if (!_matches.Any())
            {
                return;
            }

            File.WriteAllText(FileName, JsonConvert.SerializeObject(_matches));
            Logger.Log($"{FileName} saved.");
            Logger.Log($"{_matches.Count} matches written to disk.");
        }
    }
}