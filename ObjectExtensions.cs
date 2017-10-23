#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#endregion

namespace ToolBox {
    public static class ObjectExtensions {
        public static T UpdateObject<T>(this T target, T src) {
            var properties = typeof(T).GetProperties();
            foreach (var info in properties.Where(propertyInfo => propertyInfo.GetValue(src) != null && propertyInfo.PropertyType.IsDataType())) {
                info.SetValue(target, info.GetValue(src));
            }
            return target;
        }
        public static T ProjectFieldRecursively<T>(T target, T src) {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            if (src == null) return target;
            foreach (var fieldInfo in fields) {
                if (fieldInfo.GetValue(target) == null) {
                    fieldInfo.SetValue(target, fieldInfo.GetValue(src));
                }
                else {
                    var fieldType = fieldInfo.FieldType;
                    if (fieldType.IsPrimitive || fieldType == typeof(string) || !fieldType.IsClass) continue;
                    // complex case
                    ProjectFieldRecursively(fieldInfo.GetValue(target), fieldInfo.GetValue(src));
                }
            }
            return target;
        }
        public static bool IsDataType(this Type t) {
            return t.IsPrimitive
                   || t.IsEnum
                   || t == typeof(String)
                   || t == typeof(DateTime)
                   || t == typeof(Decimal)
                   || (t.IsGenericType && t.Name == "Nullable`1" && t.GetGenericArguments()[0].IsDataType());
        }
        public static bool IsSqlQuoteType(this Type t) {
            return t == typeof (String)
                   || t == typeof (DateTime)
                   || t == typeof (DateTime?);
        }
        public static object GetDefaultValueWithNull(this Type type) {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        public static object ParseDataByType(this object data, Type targetType){
            try{
                if (targetType == typeof (int)) return data.ToString().ToInteger();
                else if (targetType == typeof(double)) return data.ToString().ToDouble();
                else if (targetType == typeof(decimal))return data.ToString().ToDecimal();
                else if (targetType == typeof(bool)) return data.ToString().ToBoolean();
                else if (targetType == typeof(long)) return data.ToString().ToLong();
                else if (targetType == typeof(float)) return data.ToString().ToFloat();
                else if (targetType == typeof (String)) return data.ToString();
                else if (targetType == typeof(DateTime)) return data.ToString().ToDateTime();
                else if (targetType.IsGenericType 
                    && targetType.Name == "Nullable`1"
                    && targetType.GetGenericArguments()[0].IsDataType()) return data.ParseDataByType(targetType.GetGenericArguments()[0]);
                else return null;
            }
            catch{
                return null;
            }
        }
        //used in dynamic linq on selection builder
        public static IList<dynamic> ToSingleObjectList(dynamic t) {
            return new List<dynamic>() { t };
        }
        
    }
}
