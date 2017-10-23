using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Common.Extensions;

namespace ToolBox {
    public static class EnumerableExtensions {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector) {
            return source.DistinctBy(keySelector, null);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer) {
            HashSet<TKey> knownKeys = new HashSet<TKey>(comparer);
            foreach (TSource element in source) {
                if (knownKeys.Add(keySelector(element))) {
                    yield return element;
                }
            }
        }
        public static IPagingList<T> ToPagingList<T>(this IEnumerable<T> list, int pageNumber, int pageSize, int totalCount) {
            return new PagingList<T>(list, pageNumber, pageSize, totalCount);
        }
        public static IEnumerable<T> WhereMany<T>(this IEnumerable<T> queryable, IList<Expression<Func<T, bool>>> whereExpressions) {
            if (whereExpressions.HasItems()) {
                whereExpressions.ForEach(where =>
                    queryable = queryable.Where(where.Compile()));
            }
            return queryable;
        }
        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector, IComparer<TKey> comparer) {
            if (source == null) throw new ArgumentNullException("source");
            if (selector == null) throw new ArgumentNullException("selector");
            comparer = comparer ?? Comparer<TKey>.Default;
            //return source.Aggregate((max, current) => {
            //    var maxKey = selector(max);
            //    var candidateProjected = selector(current);
            //    if (comparer.Compare(candidateProjected, maxKey) > 0) {
            //        return current;
            //    }
            //});
            using (var sourceIterator = source.GetEnumerator()) {
                if (!sourceIterator.MoveNext()) {
                    throw new InvalidOperationException("Sequence contains no elements");
                }
                var max = sourceIterator.Current;
                var maxKey = selector(max);
                while (sourceIterator.MoveNext()) {
                    var candidate = sourceIterator.Current;
                    var candidateProjected = selector(candidate);
                    if (comparer.Compare(candidateProjected, maxKey) > 0) {
                        max = candidate;
                        maxKey = candidateProjected;
                    }
                }
                return max;
            }
        }
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector, IComparer<TKey> comparer) {
            if (source == null) throw new ArgumentNullException("source");
            if (selector == null) throw new ArgumentNullException("selector");
            comparer = comparer ?? Comparer<TKey>.Default;

            using (var sourceIterator = source.GetEnumerator()) {
                if (!sourceIterator.MoveNext()) {
                    throw new InvalidOperationException("Sequence contains no elements");
                }
                var min = sourceIterator.Current;
                var minKey = selector(min);
                while (sourceIterator.MoveNext()) {
                    var candidate = sourceIterator.Current;
                    var candidateProjected = selector(candidate);
                    if (comparer.Compare(candidateProjected, minKey) < 0) {
                        min = candidate;
                        minKey = candidateProjected;
                    }
                }
                return min;
            }
        }
    }
}