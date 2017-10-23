using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox {
    public static class DbCommandExtensions {
        public static DbCommand AddParameter(this DbCommand cmd, string name, object value) {
            var limitParam = cmd.CreateParameter();
            limitParam.ParameterName = name;
            limitParam.Value = value;
            cmd.Parameters.Add(limitParam);
            return cmd;
        }
    }
}
