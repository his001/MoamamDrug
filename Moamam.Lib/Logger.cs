using System;
using System.IO;
using System.Text;
using System.Web;
using System.Configuration;

namespace Moamam.Lib
{
    public class SysLogger
    {
        public static string LogName { get; set; }
        public static string LogPath { get; set; }

        static SysLogger()
        {
            string logPath = HttpContext.Current.Server.MapPath("~/Log");

            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);

            LogPath = logPath;
            LogName = ConfigurationManager.AppSettings["SysName"].ToString();
        }

        public static void WriteLine()
        {
            WriteLog(null, "================================================================");
        }

        public static void WriteLog(string message)
        {
            WriteLog(null, message);
        }

        public static void WriteLog(string functionName, string message)
        {
            string logFileName = string.Format("{0}\\{1}-{2:yyyyMMdd}.log", LogPath, LogName, DateTime.Now);
            string logMessage = ""; 

            if (!string.IsNullOrEmpty(functionName))
                logMessage = logMessage +  "\r\n" + string.Format("[{0}]: {1}", functionName, message);
            else
                logMessage = logMessage + "\r\n" + message;
             
            Logger.Write(logFileName, logMessage);
        }
    }

    public sealed class Logger
    {
        public static bool Clear(string filename)
        {
            FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            fs.Close();
            fs.Dispose();

            return true;
        }

        public static bool Write(string filename, string message)
        {
            return Write(filename, message, true);
        }

        public static bool Write(string filename, string message, bool showTime)
        {
            bool result = false;

            //StreamWriter w = File.AppendText(filename);
            using (FileStream fs = File.Open(filename, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                try
                {
                    //w.WriteLine("{0} > {1}", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"), message);
                    string logStr;

                    logStr = "\r\n================================================================\r\n";
                    if (showTime)
                        logStr += string.Format("{0} > {1}\r\n", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"), message);
                    else
                        logStr += string.Format("{0}\r\n", message);
                    logStr += "================================================================";

                    Byte[] info = new UTF8Encoding(true).GetBytes(logStr);
                    fs.Write(info, 0, info.Length);

                    result = true;
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
