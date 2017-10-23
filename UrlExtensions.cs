#region

using System;
using System.IO;
using System.Text.RegularExpressions;
using ServiceStack.WebHost.Endpoints;

#endregion

namespace ToolBox {
    public static class UrlExtensions {
        public static bool IsValidDomainName(this string str) {
            var regex = new Regex(@"^((?:https?\:\/\/|www\.)(?:[-a-z0-9]+\.)*[-a-z0-9]+.*)$", RegexOptions.IgnoreCase);
            return regex.IsMatch(str);
        }
        public static bool IsFromBaseDomain(this string source, string target) {
            if (!(source.IsValidDomainName() && target.IsValidDomainName())) return false;
            var url1 = new Uri(source.ToLower());
            var url2 = new Uri(target.ToLower());
            var b1 = url1.Host == url2.Host;
            var b2 = url1.AbsolutePath.StartsWith(url2.AbsolutePath);
            return b1 & b2;
        }
        public static Uri HostVirtualPath(this string str) {
            var root = new Uri(EndpointHost.VirtualPathProvider.RootDirectory.VirtualPath);
            return new Uri(root, str); 
        }
        public static string HostRealPath(this string str) {
            var root = EndpointHost.VirtualPathProvider.RootDirectory.RealPath;
            str = str.Replace("/", "\\");
            if (str.StartsWith("\\")) str = str.Substring(1); //remove first slash or Path.Combine doesn't work
            return Path.Combine(root, str);
        }
        public static string HostBaseUrl(this string str) {
            var uri = new Uri(str);
            return uri.Scheme + "://" + uri.Host + (uri.Port > 0 ? ":" + uri.Port.ToString() : "");
        }
    }
}
