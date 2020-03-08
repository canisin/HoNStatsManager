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

        public static MatchDb FromDisk()
        {
            var matchDb = new MatchDb();
            matchDb.Read();
            return matchDb;
        }

        public static MatchDb FromUpdate()
        {
            var matchDb = new MatchDb();
            matchDb.Read();
            matchDb.Download();
            return matchDb;
        }

        public static MatchDb FromWeb()
        {
            var matchDb = new MatchDb();
            matchDb.Download();
            return matchDb;
        }

        public void Read()
        {
            _matches = JsonConvert.DeserializeObject<List<Match>>(File.ReadAllText(FileName));

            Logger.Log($"Matches read from file: {_matches.Count}");
            Logger.Log($"Last match id and date: {_matches.Last().Id} - {_matches.Last().Date}");
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

            if (_matches.Any())
                File.AppendAllText(FileName, JsonConvert.SerializeObject(matches));
            else
                File.WriteAllText(FileName, JsonConvert.SerializeObject(matches));

            _matches.AddRange(matches);
        }
    }
}