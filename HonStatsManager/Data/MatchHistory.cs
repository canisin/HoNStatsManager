using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace HonStatsManager.Data
{
    internal struct MatchRecord
    {
        public string Id { get; }
        public DateTime Date { get; }

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
            return token.SelectMany(t =>
                ((string) t["history"])
                .Split(',')
                .Select(item => item.Split('|'))
                .Select(item => new MatchRecord(item.First(), ParseDate(item.Last()))));
        }

        private static DateTime ParseDate(string value)
        {
            return DateTime.ParseExact(value, "MM/dd/yyyy", null);
        }
    }
}