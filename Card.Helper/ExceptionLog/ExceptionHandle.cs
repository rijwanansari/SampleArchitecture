using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Helper.ExceptionLog
{
    public static class ExceptionHandle
    {
        public static void PrintException(Exception exception)
        {
            var st = new StackTrace(exception, true);
            var frame = st.GetFrame(0);
            var line = frame.GetFileLineNumber();
            var filename = frame.GetMethod().DeclaringType.FullName;
            var method = frame.GetMethod().DeclaringType.Name;
            string fullPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();

            string date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;

            fullPath = fullPath + "Logs";
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            string filePath = fullPath + "\\" + date + "_ErrorLog.txt";
            if (!File.Exists(filePath))
            {
                TextWriter sw = new StreamWriter(filePath);
                sw.WriteLine("File :-" + filename);
                sw.WriteLine("line :-" + line);
                sw.WriteLine("Method :-" + method);
                sw.WriteLine("Date Time :-" + DateTime.Now);
                sw.WriteLine("Message :-" + exception.Message);
                sw.WriteLine("InnerException :-" + exception.InnerException);
                sw.Close();
            }
            else
            {
                string oldLine = File.ReadAllText(filePath);
                TextWriter sw = new StreamWriter(filePath);
                sw.WriteLine(oldLine);
                sw.WriteLine(sw.NewLine);
                sw.WriteLine("File :-" + filename);
                sw.WriteLine("line :-" + line);
                sw.WriteLine("Method :-" + method);
                sw.WriteLine("Date Time :-" + DateTime.Now);
                sw.WriteLine("Message :-" + Convert.ToString(exception.Message));
                sw.WriteLine("InnerException :-" + Convert.ToString(exception.InnerException) + "---: StackTrace" + Convert.ToString(exception.StackTrace));
                sw.Close();
            }
        }
    }
}
