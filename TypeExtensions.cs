using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox {
    public static class TypeExtensions {
        public static Expression<Func<T, bool>> GetByFieldFactory<T>(this Type type, string field, object value) {
            var propertyInfos = type.GetProperties();
            var memberInfo = propertyInfos.FirstOrDefault(p => p.Name == field);
            if (memberInfo != null) {
                var newExpr = Expression.New(type);
                var memberAccess = Expression.MakeMemberAccess(newExpr, memberInfo);
                var equalExpr = Expression.Equal(memberAccess, Expression.Constant(value));
                return Expression.Lambda<Func<T, bool>>(equalExpr);
            }
            return null;
        }
        public static Expression<Func<T, bool>> GetByFieldFactory<T>(this Type type, string field, T value) {
            var propertyInfos = type.GetProperties();
            var memberInfo = propertyInfos.FirstOrDefault(p => p.Name == field);
            if (memberInfo != null) {
                var newExpr = Expression.New(type);
                var memberAccess = Expression.MakeMemberAccess(newExpr, memberInfo);
                var equalExpr = Expression.Equal(memberAccess, Expression.Constant(memberInfo.GetValue(value)));
                return Expression.Lambda<Func<T, bool>>(equalExpr);
            }
            return null;
        } 
    }
}
