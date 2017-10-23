using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox {
    public static class DataRowExtensions {
        public static object Get(this DataRow data, string field) {
            if (data == null) return null;
            return data.IsNull(field) ? "" : data[field];
        }
    }
}
