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

        private readonly List<Match> _matches = new List<Match>();

        public static MatchDb FromDisk()
        {
            var matchDb = new MatchDb();
            matchDb.Read();
            return matchDb;
        }

        public static MatchDb FromDiskWithUpdate(bool save = true)
        {
            var matchDb = new MatchDb();
            matchDb.Read();
            matchDb.Download();

            if (save)
                matchDb.Write();

            return matchDb;
        }

        public static MatchDb FromWeb(bool save = true)
        {
            var matchDb = new MatchDb();
            matchDb.Download();

            if (save)
                matchDb.Write();

            return matchDb;
        }

        public void Read()
        {
            if (!File.Exists(FileName))
            {
                Logger.Log($"{FileName} not found.");
                return;
            }

            var matches = JsonConvert.DeserializeObject<List<Match>>(File.ReadAllText(FileName));

            Logger.Log($"Matches read from file: {matches.Count}");
            Logger.Log($"Last match id and date: {matches.Last().Id} - {matches.Last().Date}");

            _matches.AddRange(matches);
        }

        public void Download()
        {
            var lastKnownDate = _matches.LastOrDefault()?.Date;

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

            _matches.AddRange(matches);
        }

        public void Write()
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