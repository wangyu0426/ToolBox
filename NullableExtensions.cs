#region

using System;
using System.Text.RegularExpressions;
using ServiceStack.Net30;

#endregion

namespace ToolBox {
    public static class NullableExtensions {

        public static bool IsBetween(this long? i, float? j, float? k) {
            if (i == null) return true;//ignore null
            return (j == null || i.GetValueOrDefault() >= j) && (k == null || i.GetValueOrDefault() <= k);
        }
        public static bool IsBetween(this int? i, float? j, float? k) {
            if (i == null) return true;//ignore null
            return (j == null || i.GetValueOrDefault() >= j) && (k == null || i.GetValueOrDefault() <= k);
        }
        public static bool IsBetween(this float? i, float? j, float? k) {
            if (i == null) return true;//ignore null
            return (j == null || i.GetValueOrDefault() >= j) && (k == null || i.GetValueOrDefault() <= k);
        }
        public static bool IsBetween(this decimal? i, float? j, float? k) {
            if (i == null) return true;//ignore null
            return (j == null || i.GetValueOrDefault() >= (decimal)j) && (k == null || i.GetValueOrDefault() <= (decimal)k);
        }
        public static bool IsSmallerThan(this long? i, long? j) {
            if (i == null || j == null) return true;//ignore null
            return i.GetValueOrDefault() < j;
        }
        public static bool IsBiggerThan(this long? i, long? j) {
            if (i == null || j == null) return true;//ignore null
            return i.GetValueOrDefault() > j;
        }

        public static bool IsSmallerThanOrEqualTo(this long? i, long? j) {
            if (i == null || j == null) return true;//ignore null
            return i.GetValueOrDefault() <= j;
        }
        public static bool IsBiggerThanOrEqualTo(this long? i, long? j) {
            if (i == null || j == null) return true;//ignore null
            return i.GetValueOrDefault() >= j;
        }

        public static bool IsBetween(this long? i, long? j, long? k) {
            if (i == null) return true;//ignore null
            return (j == null || i.GetValueOrDefault() >= j) && (k == null || i.GetValueOrDefault() <= k);
        }
        public static bool IsSmallerThan(this int? i, int? j) {
            if (i == null || j == null) return true;//ignore null
            return i.GetValueOrDefault() < j;
        }
        public static bool IsBiggerThan(this int? i, int? j) {
            if (i == null || j == null) return true;//ignore null
            return i.GetValueOrDefault() > j;
        }
        public static bool IsSmallerThanOrEqualTo(this int? i, int? j) {
            if (i == null || j == null) return true;//ignore null
            return i.GetValueOrDefault() <= j;
        }
        public static bool IsBiggerThanOrEqualTo(this int? i, int? j) {
            if (i == null || j == null) return true;//ignore null
            return i.GetValueOrDefault() >= j;
        }
        public static bool IsBetween(this int? i, int? j, int? k) {
            if (i == null) return true;//ignore null
            return (j == null || i.GetValueOrDefault() >= j) && (k == null || i.GetValueOrDefault() <= k);
        }
        public static bool IsSmallerThan(this decimal? i, decimal? j) {
            if (i == null || j == null) return true;//ignore null
            return i.GetValueOrDefault() < j;
        }
        public static bool IsBiggerThan(this decimal? i, decimal? j) {
            if (i == null || j == null) return true;//ignore null
            return i.GetValueOrDefault() > j;
        }
        public static bool IsSmallerThanOrEqualTo(this decimal? i, decimal? j) {
            if (i == null || j == null) return true;//ignore null
            return i.GetValueOrDefault() <= j;
        }
        public static bool IsBiggerThanOrEqualTo(this decimal? i, decimal? j) {
            if (i == null || j == null) return true;//ignore null
            return i.GetValueOrDefault() >= j;
        }
        public static bool IsBetween(this decimal? i, decimal? j, decimal? k) {
            if (i == null) return true;//ignore null
            return (j == null || i.GetValueOrDefault() >= j) && (k == null || i.GetValueOrDefault() <= k);
        }
        public static bool IsBetween(this DateTime? i, DateTime? j, DateTime? k) {
            if (i == null) return true;//ignore null
            return (j == null || i.GetValueOrDefault() >= j) && (k == null || i.GetValueOrDefault() <= k);
        }
        public static bool IsWithinLength(this string i, int? j, int? k) {
            if (i == null) return true;//ignore null
            return (j == null || i.Length >= j) && (k == null || i.Length <= k);
        }
        public static bool IsWithinLength(this string i, Pair<int?, int?> p) {
            if (i == null || p == null) return true;//ignore null
            return (p.Key == null || i.Length >= p.Key) && (p.Value == null || i.Length <= p.Value);
        }
        public static bool IsRegexMatch(this string s, string regx, RegexOptions opt = RegexOptions.None) {
            if (s == null || regx == null) return true;//ignore null
            return Regex.IsMatch(s, regx, opt);
        }
    }
}
