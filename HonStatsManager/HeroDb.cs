using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace HonStatsManager
{
    internal class HeroDb
    {
        public const string FileName = @"heroes.db";

        public IReadOnlyList<Hero> Heroes => _heroes.AsReadOnly();

        private readonly List<Hero> _heroes = new List<Hero>();

        public static HeroDb FromDisk()
        {
            var heroDb = new HeroDb();
            heroDb.Read();
            return heroDb;
        }

        public static HeroDb FromDiskWithUpdate(bool save = true)
        {
            var heroDb = new HeroDb();
            heroDb.Read();
            heroDb.Download();

            if (save)
                heroDb.Write();

            return heroDb;
        }

        public static HeroDb FromWeb(bool save = true)
        {
            var heroDb = new HeroDb();
            heroDb.Download();

            if (save)
                heroDb.Write();

            return heroDb;
        }

        public void Read()
        {
            if (!File.Exists(FileName))
            {
                Logger.Log($"{FileName} not found.");
                return;
            }

            var heroes = JsonConvert.DeserializeObject<List<Hero>>(File.ReadAllText(FileName));

            Logger.Log($"Heroes read from file: {heroes.Count}");

            _heroes.AddRange(heroes);
        }

        public void Download()
        {
            using (var client = new WebClient())
            {
                const string url = @"http://www.heroesofnewerth.com/heroes/";
                var content = client.DownloadString(url);

                var regex = new Regex(@"""/heroes/view/(\d+)/(.*?)""");
                var heroes = regex.Matches(content)
                    .Cast<System.Text.RegularExpressions.Match>()
                    .Select(match => new Hero(match.Groups[1].Value, match.Groups[2].Value))
                    .ToList();

                Logger.Log($"Heroes downloaded: {heroes.Count}");

                _heroes.AddRange(heroes); //todo unique
            }
        }

        public void Write()
        {
            if (!_heroes.Any())
            {
                return;
            }

            File.WriteAllText(FileName, JsonConvert.SerializeObject(_heroes));
            Logger.Log($"{FileName} saved.");
            Logger.Log($"{_heroes.Count} heroes written to disk.");
        }
    }
}