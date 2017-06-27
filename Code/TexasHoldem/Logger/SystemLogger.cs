using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public static class SystemLogger
    {
        private static string path = @"C:\Users\naordalal\Desktop\";
        public static void Log(string message , string file)
        {
            using (StreamWriter w = File.AppendText(path + file))
            {
                Log(message, w);
            }
        }

        private static void Log(string message, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", message);
            w.WriteLine("-------------------------------");
        }

        public static string getLogFile(string file)
        {
            using (StreamReader r = File.OpenText(path + file))
            {
                return readLog(r);
            }
        }

        private static string readLog(StreamReader r)
        {
            string line , data = "";
            while ((line = r.ReadLine()) != null)
            {
                data += line + "\n";
            }

            return data;
        }
    }
}
