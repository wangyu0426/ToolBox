#region

using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Common.Extensions;

#endregion

namespace ToolBox {
    public static class CollectionExtensions {
        public static ICollection<T> AddRange<T>(this ICollection<T> target, IEnumerable<T> source) {
            source.ForEach(target.Add);
            return target;
        }
        public static ICollection<T> RemoveRange<T>(this ICollection<T> target, IEnumerable<T> source) {
            source.ForEach(t1 => target.Remove(t1));
            return target;
        }
        public static bool IsCollectionType(this Type type) {
            return (type.GetInterface("ICollection") != null);
        }
        public static bool IsEnumerableType(this Type type) {
            return (type.GetInterface("IEnumerable") != null);
        }
        public static bool IsGrouping(this Type type) {
            return (type.GetInterface("IGrouping`2") != null);
        }
        public static bool HasItems<T>(this ICollection<T> list) {
            return list != null && list.Any();
        }
    }
}
