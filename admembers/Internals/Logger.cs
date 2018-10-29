using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMembers.Internals
{
    internal class Logger
    {
        protected const string CompanyName = "VareNo.Consulting";
        protected const string ApplicationName = "ADMembers";
        private static object _logFileLocker = new object();
        private static bool _pathExists = false;
        public enum Severity { Exception, Info };
        public static void Log(Exception ex)
        {
            try
            {
                CheckLogFile();
                Log("Message: {1}{0}StackTrace: {2}", Severity.Exception, Environment.NewLine, ex.Message, ex.StackTrace);
            }
            catch { }
        }

        private static void Log(string message, Severity severity, params object[] parameters)
        {
            string logMessage = string.Format(message, parameters);
            string svrty = "";
            switch (severity)
            {
                case Severity.Exception:
                    svrty = "<Error>";
                    break;
                case Severity.Info:
                default:
                    svrty = "<Info >";
                    break;
            }
            lock (_logFileLocker)
            {
                File.AppendAllText(GetLogFileName(), string.Format("{1:HH-mm-ss}{2} : {0}", logMessage, DateTime.Now, svrty) + Environment.NewLine);
            }
        }

        public static string GetLogFileName()
        {
            return Path.Combine(LogFilePath(), string.Format("Log_{0:yyyy-MM-dd}.txt", DateTime.Now));
        }

        private static string LogFilePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), CompanyName, ApplicationName);
        }

        private static void CheckLogFile()
        {
            if (!_pathExists)
            {
                var items = LogFilePath().Split('\\');
                var idx = 0;
                var path = "";
                foreach (var item in items)
                {
                    
                    if (idx++ > 3)
                    {
                        path = path + "\\" + item;
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                    }
                    else if(idx == 1)
                    {
                        path = item;
                    }
                    else
                    {
                        path = path + "\\" + item;
                    }
                }
            }
        }

        //protected async Task Log(string message, params object[] parameters)
        //{
        //    await Task.Run(() =>
        //    {
        //        var logMessage = string.Format(message, parameters);
        //        lock (_logFileLocker)
        //        {

        //            File.AppendAllText(LogFileName, string.Format("{1:HH-mm-ss} : {0}", logMessage, DateTime.Now) + Environment.NewLine);
        //        }

        //    });
        //}
    }
}
