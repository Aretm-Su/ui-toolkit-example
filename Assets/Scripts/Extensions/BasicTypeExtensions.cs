using System;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class BasicTypeExtensions
    {
        public static int SecondsToMilliseconds(this float seconds)
        {
            return Mathf.RoundToInt(seconds * 1000f);
        }

        public static bool HasSymbols(this string source)
        {
            return !String.IsNullOrEmpty(source);
        }

        public static bool NotContains<TElement>(this IList<TElement> source, TElement element)
        {
            return !source.Contains(element);
        }

        public static void AddIfNotContains<TElement>(this IList<TElement> source, TElement element)
        {
            if (!source.Contains(element))
            {
                source.Add(element);
            }
        }

        public static bool NotContainsKey<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
        {
            return !source.ContainsKey(key);
        }
    }
}