using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileVader
{
    public class Logger
    {

        private string log_path;       

        public string Log_path
        {
            get { return log_path; }
            set { log_path = value; }
        }

        public Logger(string path)
        {
            this.log_path = path;
        }

        public Logger()
        {
        }

        public void SaveEvent(string message)
        {
            using (TextWriter twError = File.AppendText(this.log_path + @"\" + "FileDestroyer_" + convertDateToFileName(DateTime.Now) + ".log"))
            {
                Log(message, twError);
            }
        }

        public void SaveDefault(string message)
        {
            using (TextWriter twError = File.AppendText("FileDestroyer_" + convertDateToFileName(DateTime.Now) + ".log"))
            {
                Log(message, twError);
            }
        }

        private static void Log(String logMessage, TextWriter w)
        {
            w.WriteLine(convertDate(DateTime.Now) + "\t" + convertTime(DateTime.Now) + "\t" + logMessage);
            w.Flush();            
        }

        private static string convertDate(DateTime date)
        {
            string temp = string.Format("{0:dd/MM/yyyy}", date);
            return temp;
        }

        private static string convertTime(DateTime date)
        {
            string temp = string.Format("{0:hh:mm:ss}", date);
            return temp;
        }

        public static string convertDateToFileName(DateTime date)
        {
            string temp = string.Format("{0:yyyy/MM/dd}", date);
            temp = temp.Replace(' ', ' ');
            temp = temp.Replace('/', '-');
            temp = temp.Replace(':', '-');
            return temp;
        }

        public static string convertDateToMonthFileName(DateTime date)
        {
            string temp = string.Format("{0:yyyy/MM}", date);
            temp = temp.Replace(' ', ' ');
            temp = temp.Replace('/', '-');
            temp = temp.Replace(':', '-');
            return temp;
        }

    }
}
