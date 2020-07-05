using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace HonStatsManager
{
    internal static class HeroDb
    {
        public const string Url = @"http://www.heroesofnewerth.com/heroes/";
        public const string FileName = @"heroes.db";

        public static IReadOnlyList<Hero> Heroes => HeroesStore.AsReadOnly();

        private static readonly List<Hero> HeroesStore = new List<Hero>();

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

        public static void Read()
        {
            if (!File.Exists(FileName))
            {
                Logger.Log($"{FileName} not found.");
                return;
            }

            var heroes = JsonConvert.DeserializeObject<List<Hero>>(File.ReadAllText(FileName));

            Logger.Log($"Heroes read from file: {heroes.Count}");

            HeroesStore.AddRange(heroes);
        }

        public static void Download()
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

                HeroesStore.AddRange(heroes); //todo unique
            }
        }

        public static void Write()
        {
            if (!HeroesStore.Any())
            {
                return;
            }

            File.WriteAllText(FileName, JsonConvert.SerializeObject(HeroesStore));
            Logger.Log($"{FileName} saved.");
            Logger.Log($"{HeroesStore.Count} heroes written to disk.");
        }
    }
}