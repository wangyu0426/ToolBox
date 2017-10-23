#region

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ServiceStack.Common.Extensions;

#endregion

namespace ToolBox {
    public static class QueryableExtensions {
        public static IQueryable<T> IncludeMany<T>(this IQueryable<T> queryable, IList<string> includes) {
            if (includes.HasItems()){
                includes.ForEach(table =>
                    queryable = queryable.Include(table));
            }
            return queryable;
        }
        public static IQueryable<T> WhereMany<T>(this IQueryable<T> queryable, IList<Expression<Func<T, bool>>> whereExpressions) {
            if (whereExpressions.HasItems()) {
                whereExpressions.ForEach(where =>
                    queryable = queryable.Where(where));
            }
            return queryable;
        }
    }
}
