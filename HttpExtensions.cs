#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;

#endregion

namespace ToolBox {
    public enum CookiesType {
        Perm = 1,
        Temp = 0
    }
    public static partial class HttpExtensions {
        //get item from cookie or buffered items 
        public static string GetCookie(this IHttpRequest req, string name) {
            if (req == null || name == null) return null;
            //items are useful when *first* add cookies, you can't instantly get cookies values yet
            object value;
            if (req.Items.TryGetValue(name, out value)) return value.ToString();
            Cookie cookie;
            if (req.Cookies.TryGetValue(name, out cookie)) return cookie.Value;
            return null;
        }
        //add or overwrite if exists
        public static void SetCookie(this IHttpResponse res, IHttpRequest req, string name, string val, bool isPerm) {
            if (req == null || res == null || name.IsNullOrWhiteSpace()) return;
            if (isPerm)
                res.Cookies.AddPermanentCookie(name, val);
            else
                res.Cookies.AddSessionCookie(name, val,
                                             (EndpointHost.Config != null &&
                                              EndpointHost.Config.OnlySendSessionCookiesSecurely &&
                                              req.IsSecureConnection));
            //items are useful when *first* add cookies, you can't instantly get cookies values yet
            if (req.Items.ContainsKey(name))
                req.Items[name] = val;
            else
                req.Items.Add(name, val);
        }
        public static void RemoveCookie(this IHttpResponse res, IHttpRequest req, string name) {
            if (res == null || name == null) return;
            res.Cookies.DeleteCookie(name);
            if (req.Items.ContainsKey(name)) req.Items.Remove(name);
        }
        //get a set of hash
        public static HashSet<string> GetCookieHashSet(this IHttpRequest req, string name) {
            if (req == null || name == null) return new HashSet<string>();
            var str = GetCookie(req, name);
            return str == null ? new HashSet<string>() : new HashSet<string>(str.SplitTrimToList());
        }
        //set a value into hash, do nothing if exists
        public static void SetCookieHashSet(this IHttpResponse res, IHttpRequest req, string name, string val, bool isPerm) {
            if (req == null || res == null || name.IsNullOrWhiteSpace()) return;
            var hs = GetCookieHashSet(req, name);
            if (!hs.Contains(val)) {
                hs.Add(val);
                var str = hs.ToArray().JoinScopePathAndIdTrimToString();
                SetCookie(res, req, name, str, isPerm);
            }
        }
        //remove a value from hash, do nothing if not exist
        public static void RemoveCookieHashSet(this IHttpResponse res, IHttpRequest req, string name, string val, bool isPerm) {
            if (req == null || res == null || name.IsNullOrWhiteSpace()) return;
            var hs = GetCookieHashSet(req, name);
            if (hs.Contains(val)) {
                hs.Remove(val);
                var str = String.Join(",", hs.ToArray());
                SetCookie(res, req, name, str, isPerm);
            }
        }
        //get header value
        public static string GetHeader(this IHttpRequest req, string name) {
            if (req == null || name.IsNullOrWhiteSpace()) return null;
            return req.Headers[name];
        }
        public static HashSet<string> GetHeaderHashSet(this IHttpRequest req, string name) {
            if (req == null || name.IsNullOrWhiteSpace()) return null;
            var str = req.Headers[name];
            return str == null ? new HashSet<string>() : new HashSet<string>(str.Split(','));
        }
        //set header value 
        public static void SetHeader(this IHttpResponse res, string name, string val) {
            if (res == null || name.IsNullOrWhiteSpace()) return;
            res.AddHeader(name, val);
        }
        public static void SetHeaderHashSet(this IHttpResponse res, string name, HashSet<string> hs) {
            if (res == null || name.IsNullOrWhiteSpace()) return;
            var str = hs == null ? "" : String.Join(",", hs.ToArray());
            res.AddHeader(name, str);
        }
    }
}
