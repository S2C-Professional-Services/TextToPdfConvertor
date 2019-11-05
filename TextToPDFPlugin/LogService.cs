using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToPDFPlugin
{
    public static class LogService
    {
        public static void LogDetails(string content)
        {
            string exe = Process.GetCurrentProcess().MainModule.FileName;
            string path = Path.GetDirectoryName(exe);
            Directory.CreateDirectory(path + @"\Logs");
            FileStream fs = new FileStream(path + @"\Logs" + @"\ServiceLog_" + DateTime.Now.ToString("yyyyMMddHH") + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();
        }
    }
}
