using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HonStatsManager.Utility;
using Newtonsoft.Json;

namespace HonStatsManager.Data
{
    internal static class HeroDb
    {
        public const string Url = @"http://www.heroesofnewerth.com/heroes/";
        public const string FileName = @"heroes.db";

        public static IReadOnlyCollection<Hero> Heroes => _heroDict.Values;
        public static IReadOnlyDictionary<string, Hero> HeroDict => _heroDict.AsReadOnly();

        private static Dictionary<string, Hero> _heroDict = new Dictionary<string, Hero>();

        public static void Initialize()
        {
            Read();
        }

        public static void Update()
        {
            Download();
            Write();
        }

        private static void Read()
        {
            if (!File.Exists(FileName))
            {
                Logger.Log($"{FileName} not found.");
                return;
            }

            var heroes = JsonConvert.DeserializeObject<List<Hero>>(File.ReadAllText(FileName));

            Logger.Log($"Heroes read from file: {heroes.Count}");

            _heroDict = heroes.ToDictionary(hero => hero.Id);
        }

        private static void Download()
        {
            using (var client = new WebClient())
            {
                var content = client.DownloadString(Url);

                var regex = new Regex(@"""/heroes/view/(\d+)/(.*?)""");
                var heroes = regex.Matches(content)
                    .Cast<System.Text.RegularExpressions.Match>()
                    .Select(match => new Hero(match.Groups[1].Value, match.Groups[2].Value))
                    .ToList();

                Logger.Log($"Heroes downloaded: {heroes.Count}");

                _heroDict = heroes.ToDictionary(hero => hero.Id);
            }
        }

        private static void Write()
        {
            if (!_heroDict.Any())
            {
                return;
            }

            File.WriteAllText(FileName, JsonConvert.SerializeObject(_heroDict.Values));
            Logger.Log($"{FileName} saved.");
            Logger.Log($"{_heroDict.Count} heroes written to disk.");
        }
    }
}