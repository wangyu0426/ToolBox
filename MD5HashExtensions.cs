#region

using System.Security.Cryptography;
using System.Text;

#endregion

namespace ToolBox {
    public static class Md5HashExtensions {
        public static string Md5Hash(this string s) {
            if (s.IsNullOrWhiteSpace()) return null;
            MD5 md5 = MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            byte[] hash = md5.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++) sb.Append(hash[i].ToString("x2")); //"X2" for upper-case, "x2" for lower-case output
            return sb.ToString();
        }
    }
}
