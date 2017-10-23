#region

using System;

#endregion

namespace ToolBox {
    public static class DateTimeExtensions {
        public static string ToIso8601String(this DateTime? d) {
            if (d == null) return DateTime.MinValue.ToString("o");
            return d.GetValueOrDefault().ToString("o"); //without "Z"
            // return d.GetValueOrDefault().ToUniversalTime().ToString("o"); Gives 2009-06-11T16:11:10.5312500Z
        }
        public static string ToIso8601String(this DateTime d) {
            return d.ToString("o"); //without "Z"
            // return d.GetValueOrDefault().ToUniversalTime().ToString("o"); Gives 2009-06-11T16:11:10.5312500Z
        }
        public static string ToUtcTimeString(this DateTime d) {
            return d.ToString("yyyy-MM-ddThh:mm:ss"); //without "Z"
            // return d.GetValueOrDefault().ToUniversalTime().ToString("o"); Gives 2009-06-11T16:11:10.5312500Z
        }
    }
}
