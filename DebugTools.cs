using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox {
    public static class DebugTools {
        private static DateTime Time { get; set; }
        public static void TextMsg(string msg){
            Debug.WriteLine("{0}:{1}",msg, TimePassed());
        }
        private static int TimePassed(){
            if (Time == DateTime.MinValue) Time = DateTime.Now;
            var now = DateTime.Now;
            var pre = Time;
            Time = now;
            return (int)now.Subtract(pre).TotalMilliseconds;
        }
    }
}
