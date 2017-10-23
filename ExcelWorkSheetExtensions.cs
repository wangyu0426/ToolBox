using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;
namespace ToolBox {
    public static class ExcelWorkSheetExtensions {
        public static ExcelRange AssignColumnValue(this ExcelWorksheet ws, int fromR, int fromC, int toR, int toC, object value) {
            var range = ws.Cells[fromR, fromC, toR, toC];
            ws.Cells[fromR, fromC].Value = value; // Heading Name
            range.Merge = true; //Merge columns start and end range
            return range;
        }
        public static ExcelRange AssignColumnValue(this ExcelWorksheet ws, int fromR, int fromC,object value) {
            var range = ws.Cells[fromR, fromC];
            range.Value = value; // Heading Name
            return range;
        }
        public static ExcelRange AssignStyle(this ExcelRange range, bool bold, bool italic, int size,ExcelHorizontalAlignment alignment) {
            range.Style.Font.Bold = bold; //Font should be bold
            range.Style.Font.Italic = italic; //Font should be bold
            range.Style.Font.Size = size;
            range.Style.HorizontalAlignment = alignment; // Aligmnet 
            return range;
        }
    }
}
