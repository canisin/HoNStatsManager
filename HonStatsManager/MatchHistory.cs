using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace HonStatsManager
{
    internal class MatchRecord
    {
        public string Id;
        public DateTime Date;
    }

    internal static class MatchHistory
    {
        public static IEnumerable<MatchRecord> Parse(JToken token)
        {
            return ((string) token[0]["history"])
                .Split(',')
                .Select(item => item.Split('|'))
                .Select(item => new MatchRecord
                {
                    Id = item.First(),
                    Date = ParseDate(item.Last())
                });
        }

        private static DateTime ParseDate(string value)
        {
            return DateTime.ParseExact(value, "MM/dd/yyyy", null);
        }
    }
}