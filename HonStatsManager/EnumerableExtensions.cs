﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HonStatsManager
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<List<T>> SplitBy<T>(this IEnumerable<T> seq, int splitCount)
        {
            if (splitCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(splitCount), splitCount,
                    "Argument splitCount must be positive.");

            var temp = new List<T>(splitCount);

            foreach (var item in seq)
            {
                temp.Add(item);

                if (temp.Count != splitCount)
                    continue;

                yield return temp;
                temp.Clear();
            }

            if (temp.Any())
                yield return temp;
        }
    }
}