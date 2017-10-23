#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace ToolBox {
    public static class StringExtensions {
        public static bool IsEqualOrWhiteSpace(this string str1, string str2) {
            return (str1.IsNullOrWhiteSpace() && str2.IsNullOrWhiteSpace()) || str1 == str2;
        }
        public static bool IsNullOrWhiteSpace(this string str) {
            return string.IsNullOrWhiteSpace(str);
        } 
        public static bool IsLong(this string s) {
            if (s.IsNullOrWhiteSpace()) return false;
            long l;
            return Int64.TryParse(s, out l);
        }
        public static bool IsInteger(this string s) {
            if (s.IsNullOrWhiteSpace()) return false;
            int i;
            return Int32.TryParse(s, out i);
        }
        public static bool IsDouble(this string s) {
            if (s.IsNullOrWhiteSpace()) return false;
            double i;
            return Double.TryParse(s, out i);
        }
        public static bool IsNumeric(this string s) {
            if (s.IsNullOrWhiteSpace()) return false;
            return s.IsInteger() || s.IsDouble();
        }
        public static bool IsBoolean(this string s) {
            if (s.IsNullOrWhiteSpace()) return false;
            int i;
            if (Int32.TryParse(s, out i)) {
                if (i == 0 || i == 1) return true;
            }
            bool b;
            return Boolean.TryParse(s, out b);
        }
        public static bool Like(this string target, string source) {
            return target.IndexOf(source, StringComparison.OrdinalIgnoreCase) >= 0;
        }
        public static Expression<Func<string,string,bool>>  LikeExpression = (target,source)=>source.IndexOf(target, StringComparison.OrdinalIgnoreCase) >= 0;
        public static Size ToSize(this string s) {
            var size = new Size(0, 0);
            if (s.IsNullOrWhiteSpace()) return size;
            var arr =s.DeserializeJson<IList<int>>();
            if (arr.Count < 2) return size;
            return new Size(arr[0] > 0 ? arr[0] : 0, arr[1] > 0 ? arr[1] : 0);
        }
        //this exists in ServiceStack.Common already
        //public static T ToEnum<T>(this string s) where T : struct, IConvertible {
        //    if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");
        //    T e;
        //    if (Enum.TryParse(s, true, out e)) return e;      
        //    return default(T);
        //}
        public static long ToLong(this string s) {
            long l = 0;
            if (!s.IsNullOrWhiteSpace()) Int64.TryParse(s, out l);
            return l;
        }
        public static int ToInteger(this string s) {
            int i = 0;
            if (!s.IsNullOrWhiteSpace()) Int32.TryParse(s, out i);
            return i;
        }
        public static double ToDouble(this string s) {
            double d = 0;
            if (!s.IsNullOrWhiteSpace()) Double.TryParse(s, out d);
            return d;
        }
        public static float ToFloat(this string s) {
            float d = 0;
            if (!s.IsNullOrWhiteSpace()) float.TryParse(s, out d);
            return d;
        }
        public static decimal ToDecimal(this string s) {
            decimal d = 0;
            if (!s.IsNullOrWhiteSpace()) Decimal.TryParse(s, out d);
            return d;
        }
        public static DateTime ToDateTime(this string s) {
            DateTime d = DateTime.MinValue;
            if (!s.IsNullOrWhiteSpace()) DateTime.TryParse(s, out d);
            return d;
        }
        public static DateTime ToDateTime(this string s,string format, CultureInfo providor)
        {
            DateTime d = DateTime.UtcNow;
            if (!s.IsNullOrWhiteSpace()) DateTime.TryParseExact(s, format, providor, DateTimeStyles.None, out d);
            if(d<new DateTime(1753,01,01)) d = DateTime.UtcNow;
            return d;
        }
        public static bool ToBoolean(this string s) {
            bool b = false;
            if (!s.IsNullOrWhiteSpace()) {
                int i;
                if (Int32.TryParse(s, out i)) {
                    if (i >= 1) b = true;
                } else {
                    Boolean.TryParse(s, out b);
                }
            }
            return b;
        }
        public static IList<string> SplitScopePathAndIdTrimToList(this string str) {
            return (str ?? "").Split('/').Select(x => x.Trim()).ToList();
        }
        public static IList<string> SplitTrimToList(this string str) {
            return (str ?? "").Split(',').Select(x => x.Trim()).ToList();
        }
        public static IList<string> SplitTrimToArgList(this string str) {
            return (str ?? "").Split(']').Select(x => x.Trim()).Where(x=>!x.IsNullOrWhiteSpace()).ToList();
        }
        public static string JoinToArgTrimString(this IEnumerable<string> lst) {
            if (lst == null) return "";
            var str = String.Join("]", lst.Select(x => x.Trim()).ToList());
            return str.IsNullOrWhiteSpace()?"":str+"]";
        }
        public static string JoinScopePathAndIdTrimToString(this IEnumerable<string> lst) {
            if (lst == null) return "";
            return String.Join("/", lst.Select(x=>x.Trim()).ToList());            
        }
        public static string JoinToTrimString(this IEnumerable<string> lst) {
            if (lst == null) return "";
            return String.Join(",", lst.Select(x => x.Trim()).ToList());
        }
        public static string JoinToTrimString(this IEnumerable<int> lst) {
            if (lst == null) return "";
            return String.Join(",", lst.ToList());
        }
        public static bool IsSubsetOf(this string sub, string sup, StringComparison cmp = StringComparison.OrdinalIgnoreCase) {
            return sub.SplitTrimToList().IsSubsetOf(sup.SplitTrimToList(), (x, y) => x.Equals(y, cmp));
        }
        public static bool ContentEquals(this string str1, string str2, StringComparison cmp = StringComparison.OrdinalIgnoreCase) {
            return str1.SplitTrimToList().ContentEquals(str2.SplitTrimToList(), (x, y) => x.Equals(y, cmp));
        }
        public static IList<string> TrimPath(this string str) {
            return (str ?? "").Split('\\').Select(x => x.Trim()).ToList();
        }
        public static string CamelCaseToPascalCase(this string str) {
            return str.IsNullOrWhiteSpace() ? str : str.Substring(0, 1).ToUpper() + (str.Length > 1 ? str.Substring(1) : "");
        }
        public static string ToProperCase(this string str) {
            return String.Join(" ", str.Split(' ').Select(word => word.ToLower().CamelCaseToPascalCase()).ToList());
        }
        public static string ReverseLines(this string str) {
            var lines = str.Split('\n');
            var rlines = lines.Reverse();
            var sb = new StringBuilder();
            foreach (var line in rlines) {
                sb.Append(line);
                sb.Append('\n');
            }
            return sb.ToString();
        }
        public static string AddDisableCache(this string str) {
            if (str.IsNullOrWhiteSpace()) return "";
            var d = DateTime.UtcNow.Ticks;
            return str + "?dc=" + d;
        }
        public static string RemoveDisableCache(this string str) {
            if (str.IsNullOrWhiteSpace()) return "";
            return str.Split('?')[0];
        }
        public static string EscapeSql(this string input) {
            input = input.Replace(@"\", @"\\");
            input = input.Replace(@"'", @"''");
            //input = input.Replace(@"%", @"\%");
            input = input.Replace(@"[", @"\[");
            input = input.Replace(@"]", @"\]");
            //input = input.Replace(@"_", @"\_");
            return input;
        }
        public static bool ContainsCaseInsensitive(this string str, string comp) {
            return str.IndexOf(comp, StringComparison.OrdinalIgnoreCase) >= 0;
        }
		public static string GetLoaleString(this string str, string locale){
           // "en_GB":"(?<text>([\S \r\n]*))"
           //" \"{0}\":\"(?<text>([\\S \\r\\n]*))\" " 
           //@" ""{0}"":""(?<text>([\S \r\n]*))"" "
            var text = "";
            var pattern = String.Format(@"""{0}"":""(?<text>([\S \r\n]*))""", locale);
            var ms = Regex.Matches(str,pattern);
            if (ms.Count <= 0) text = "";
            if (!ms[0].Groups["text"].Value.IsNullOrWhiteSpace()) {
                text = ms[0].Groups["text"].Value;
            }
            return text;
        }
        public static IList<MailAddress> ToMailAddresses(this string str) {
            if (str.IsNullOrWhiteSpace()) return new List<MailAddress>();
            return str.Split(';').Where(x => !x.IsNullOrWhiteSpace()).Select(x => new MailAddress(x)).ToList();
        }

        public static string GetSqlValue(this string value, Type fieldType) {
            string v = null;
            // pick up arguments for string and bool
            // {0} => "{0}"
            if (value == "DBNULL") return "NULL";
            if (fieldType.IsValueType) v = value;
            if (fieldType == typeof(string)) {
                v = Regex.IsMatch(value, @"^{\d+}$")
                    ? string.Format("{0}", value)
                    : string.Format("{0}", (Convert.ChangeType(value, fieldType))).EscapeSql();
            }
            if (fieldType == typeof(bool) || fieldType == typeof(bool?)) v = Regex.IsMatch(value, @"^{\d+}$") ? value : string.Format("{0}", (value.ToBoolean()) ? 1 : 0);
            if (fieldType == typeof(DateTime) || fieldType == typeof(DateTime?))
                v = value.Equals("NULL", StringComparison.OrdinalIgnoreCase) && fieldType == typeof(DateTime?) ? "NULL" : DateTime.Parse(value).ToString("yyyy-M-d");
            if (fieldType.IsEnum) v = value.ToInteger().ToString();
            return v;
        }
    }
}
