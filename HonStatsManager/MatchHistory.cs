using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace HonStatsManager
{
    internal struct MatchRecord
    {
        public readonly string Id;
        public readonly DateTime Date;

        public MatchRecord(string id, DateTime date)
        {
            Id = id;
            Date = date;
        }
    }

    internal static class MatchHistory
    {
        public static IEnumerable<MatchRecord> Parse(JToken token)
        {
            return ((string) token[0]["history"])
                .Split(',')
                .Select(item => item.Split('|'))
                .Select(item => new MatchRecord(item.First(), ParseDate(item.Last())));
        }

        private static DateTime ParseDate(string value)
        {
            return DateTime.ParseExact(value, "MM/dd/yyyy", null);
        }
    }
}