#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ServiceStack.Common.Extensions;

#endregion

namespace ToolBox {
    public static class ListExtensions {
        public static string ToString<T>(this IList<T> list, string deli) {
            var sb = new StringBuilder();
            for (var i = 0; i < list.Count; i++) {
                sb.Append(list[i].ToString());
                if (i < list.Count - 1) sb.Append(deli);
            }
            return sb.ToString();
        }
        public static bool IsSubsetOf<T>(this IList<T> subset, IList<T> superset, Func<T, T, bool> predicate) {
            return subset.All(sub => superset.Any(sup => predicate(sub, sup)));
        }
        public static bool ContentEquals<T>(this IList<T> list1, IList<T> list2, Func<T, T, bool> predicate) {
            return list1.IsSubsetOf(list2, predicate) && list2.Count == list1.Count;
        }
        public static IList<T> AppendList<T>(this IList<T> target, IList<T> source) {
            source.ForEach(target.Add);
            return target;
        } 
        public static bool HasItems<T>(this IList<T> list) {
            return list != null && list.Any();
        }

        public static IList<T> AddRange<T>(this IList<T> target, IList<T> source) {
            if(source.HasItems()) source.ForEach(target.Add);
            return target;
        }
        public static IList<T> AddOrUpdate<T>(this IList<T> target, T obj, Func<T, int, bool> compareFunc, Func<T, int, T> updateFunc) {
            var updated = false;
            for (var index = 0; index < target.Count(); index++) {
                if (compareFunc(target[index], index)) {
                    target[index] = updateFunc(target[index],index);
                    updated = true;
                }
            }
            if (!updated) target.Add(obj);
            return target;
        }
        public static IList<T> Delete<T>(this IList<T> target, Func<T, int, bool> compareFunc) {
            for (var index = 0; index < target.Count(); index++) {
                if (compareFunc(target[index], index)) {
                    target.RemoveAt(index);
                    index--;
                }
            }
            return target;
        } 
    }
}
