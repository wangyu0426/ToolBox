#region

using System;
using System.ComponentModel;

#endregion

namespace ToolBox {
    public static class EnumExtensions {
        public static string GetCustomDescription(object objEnum) {
            var fi = objEnum.GetType().GetField(objEnum.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : objEnum.ToString();
        }

        public static string Description(this Enum value) {
            return GetCustomDescription(value);
        }

        public static int Value(this Enum value) {
            return Convert.ToInt32(value);
        }
        public static string ValueString(this Enum value) {
            return Convert.ToString(value.Value());
        }
    }
}
