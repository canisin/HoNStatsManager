using System;
using System.Collections.Generic;
using System.Linq;

namespace HonStatsManager
{
    internal static class EnumerableExtensions
    {
        public static bool In<T>(this T item, params T[] seq)
        {
            return item.In((IEnumerable<T>) seq);
        }

        public static bool In<T>(this T item, IEnumerable<T> seq)
        {
            return seq.Contains(item);
        }

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

        public static IEnumerable<T> Except<T>(this IEnumerable<T> seq, params T[] items)
        {
            return seq.Except((IEnumerable<T>) items);
        }

        public static string StringJoin<T>(this IEnumerable<T> seq, string separator)
        {
            return string.Join(separator, seq);
        }

        public static IEnumerable<KeyValuePair<TKey, TSource>> ToKeyValuePairs<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.ToKeyValuePairs(keySelector, item => item);
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> ToKeyValuePairs<TSource, TKey, TValue>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)
        {
            return source.Select(item => new KeyValuePair<TKey, TValue>(keySelector(item), valueSelector(item)));
        }

        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dictionary,
            IEnumerable<KeyValuePair<TKey, TValue>> seq)
        {
            foreach (var keyValuePair in seq)
            {
                dictionary.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        public static string StringJoin(this IEnumerable<char> chars)
        {
            return new string(chars.ToArray());
        }
    }
}