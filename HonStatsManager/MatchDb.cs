using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace HonStatsManager
{
    internal static class MatchDb
    {
        public const string FileName = @"matches.db";

        public static IReadOnlyList<Match> Matches => MatchesStore.AsReadOnly();

        private static readonly List<Match> MatchesStore = new List<Match>();

        public static void InitializeFromDisk()
        {
            Read();
        }

        public static void InitializeFromDiskWithUpdate(bool save = true)
        {
            Read();
            Download();

            if (save)
                Write();
        }

        public static void InitializeFromWeb(bool save = true)
        {
            Download();

            if (save)
                Write();
        }

        private static void Read()
        {
            if (!File.Exists(FileName))
            {
                Logger.Log($"{FileName} not found.");
                return;
            }

            var matches = JsonConvert.DeserializeObject<List<Match>>(File.ReadAllText(FileName));

            Logger.Log($"Matches read from file: {matches.Count}");
            Logger.Log($"Last match id and date: {matches.Last().Id} - {matches.Last().Date}");

            MatchesStore.AddRange(matches);
        }

        private static void Download()
        {
            var lastKnownDate = MatchesStore.LastOrDefault()?.Date;

            var matchHistory = Honzor.GetMatchHistory()
                .SkipWhile(m => m.Date < lastKnownDate)
                .ToList();

            if (!matchHistory.Any())
            {
                Logger.Log(lastKnownDate.HasValue
                    ? $"No new matches since {lastKnownDate}."
                    : "No matches found.");

                return;
            }

            Logger.Log($"Found {matchHistory.Count} {(lastKnownDate.HasValue ? "new" : "")} matches.");

            var matches = HonApi.GetMultiMatch(matchHistory).ToList();

            Logger.Log($"Matches downloaded: {matches.Count}");
            Logger.Log($"Last match id and date: {matches.Last().Id} - {matches.Last().Date}");

            MatchesStore.AddRange(matches);
        }

        private static void Write()
        {
            if (!MatchesStore.Any())
            {
                return;
            }

            File.WriteAllText(FileName, JsonConvert.SerializeObject(MatchesStore));
            Logger.Log($"{FileName} saved.");
            Logger.Log($"{MatchesStore.Count} matches written to disk.");
        }
    }
}