namespace ToolBox {
    public static class CryptorExtensions {
        public static string EncryptString(this string s) {
            if (s.IsNullOrWhiteSpace()) return @"";
            return s;
        }
        public static string DecryptString(this string s) {
            if (s.IsNullOrWhiteSpace()) return @"";
            return s;
        }
    }
}
