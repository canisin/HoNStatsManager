using System;
using System.Collections.Generic;
using HonStatsManager.Data;

namespace HonStatsManager.Analysis
{
    [Flags]
    internal enum FilterType
    {
        None = 0,
        MatchTypeOneVsOne = 1,
        MatchTypeTwoVsOne = 1 << 1,
        MatchTypeTwoVsTwo = 1 << 2,
        MatchTypeThreeVsTwo = 1 << 3,
        MatchTypeOther = 1 << 4,
        MapCaldavar = 1 << 5,
        MapMidwars = 1 << 6,
        DataKicks = 1 << 7,
        DataDiscos = 1 << 8,
        DataIncomplete = 1 << 9,
        DataMissingHeroes = 1 << 10
    }

    internal static class FilterTypeExtensions
    {
        public static List<MatchType> GetFilteredMatchTypes(this FilterType filters)
        {
            var ret = new List<MatchType>();

            if (filters.HasFlag(FilterType.MatchTypeOneVsOne))
                ret.Add(MatchType.OneVsOne);

            if (filters.HasFlag(FilterType.MatchTypeTwoVsOne))
                ret.Add(MatchType.TwoVsOne);

            if (filters.HasFlag(FilterType.MatchTypeTwoVsTwo))
                ret.Add(MatchType.TwoVsTwo);

            if (filters.HasFlag(FilterType.MatchTypeThreeVsTwo))
                ret.Add(MatchType.ThreeVsTwo);

            if (filters.HasFlag(FilterType.MatchTypeOther))
                ret.Add(MatchType.Other);

            return ret;
        }

        public static List<string> GetFilteredMaps(this FilterType filters)
        {
            var ret = new List<string>();

            if (filters.HasFlag(FilterType.MapCaldavar))
                ret.Add("caldavar");

            if (filters.HasFlag(FilterType.MapMidwars))
                ret.Add("midwars");

            return ret;
        }
    }
}