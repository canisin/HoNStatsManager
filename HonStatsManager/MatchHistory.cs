using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace HonStatsManager
{
    internal class MatchHistory : List<(string Id, DateTime Date)>
    {
        public MatchHistory(JToken token)
        {
            AddRange(((string) token[0]["history"])
                .Split(',')
                .Select(item => item.Split('|'))
                .Select(item => (
                    item.First(),
                    ParseDate(item.Last()))));
        }

        private static DateTime ParseDate(string value)
        {
            return DateTime.ParseExact(value, "MM/dd/yyyy", null);
        }
    }
}