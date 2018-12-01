using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Core.Common
{
    internal class ErrorLogs
    {
        internal static void PrintError(string className
           , string methodName
           , string msg)
        {
            string layerName = "Sample.Core.Common";
            Sample.Helper.ErrorLog.Error.PrintError(layerName
                , className
                , methodName
                , msg);
        }
    }
}
