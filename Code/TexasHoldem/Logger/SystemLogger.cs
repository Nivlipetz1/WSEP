using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class SystemLogger
    {
        public void Log(string message , string fileName)
        {
            using (StreamWriter w = File.AppendText(fileName))
            {
                Log(message, w);
            }
        }

        private void Log(string message, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", message);
            w.WriteLine("-------------------------------");
        }

        public string getLogFile(string fileName)
        {
            using (StreamReader r = File.OpenText(fileName))
            {
                return readLog(r);
            }
        }

        private string readLog(StreamReader r)
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
