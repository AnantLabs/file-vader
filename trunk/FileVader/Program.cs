using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileVader
{
    class Program
    {
        private static string filepath;
        private static string logpath;
        private static string extension;
        private static double days;
        private static Logger logger;

        static void Main(string[] args)
        {
            logpath = findArg(args, "-log");
            filepath = findArg(args,"-scan");
            extension = findArg(args, "-ext");           

            PathExists(logpath, "log");
            logger = new Logger(logpath);


            PathExists(filepath, "scan");
            ExtensionExists(extension);

            days = DaysExists(findArg(args, "-days"));

            List<string> dirs = FileHelper.GetFilesRecursive(filepath, extension);

            logger.SaveEvent("INFO: Found "+ dirs.Count+" files with extension " + extension);

            if (dirs.Count > 0)
                FileHelper.RemoveFiles(dirs, days);
        }


        static class FileHelper
        {
            public static List<string> GetFilesRecursive(string b, string extension)
            {
                if (extension.Length < 1)
                    extension = "*";

                // 1.
                // Store results in the file results list.
                List<string> result = new List<string>();

                // 2.
                // Store a stack of our directories.
                Stack<string> stack = new Stack<string>();

                // 3.
                // Add initial directory.
                stack.Push(b);

                // 4.
                // Continue while there are directories to process
                while (stack.Count > 0)
                {
                    // A.
                    // Get top directory
                    string dir = stack.Pop();

                    try
                    {
                        // B
                        // Add all files at this directory to the result List.
                        result.AddRange(Directory.GetFiles(dir, "*." + extension));

                        // C
                        // Add all directories at this directory.
                        foreach (string dn in Directory.GetDirectories(dir))
                        {
                            stack.Push(dn);
                        }
                    }
                    catch
                    {
                        // D
                        // Could not open the directory
                    }
                }
                return result;
            }

            public static bool RemoveFiles(List<string> files, double days)
            {
                int count = 0;

                DateTime userDate = DateTime.Now.AddDays(0 - days);
                logger.SaveEvent("INFO: Removing files older than " + days +" days ("+ userDate.ToShortDateString()+").");

                foreach (string file in files)
                {
                    DateTime dt = Directory.GetCreationTime(file);
                    if (dt <= userDate)
                    {
                        Remove(file);
                        count++;
                    }                    
                }

                logger.SaveEvent("INFO: Removed " + count + " files");

                return true;
            }

            public static bool Remove(string path)
            {
                try
                {
                    File.Delete(path);
                    logger.SaveEvent("INFO: File removed: " + path);
                }
                catch (Exception e)
                {
                    logger.SaveEvent("ERROR: " + e.Message.ToString());
                }

                return true;
            }
        }


        private static string findArg(string[] args, string prefix)
        {
            bool found = false;

            foreach (string argument in args)
            {
                if (found)
                    return argument;

                if (argument == prefix)
                    found = true;
            }

            return "";
        }

        private static void PathExists(string path, string concept)
        {
            if (!Directory.Exists(path))
            {
                logger = new Logger();
                logger.SaveDefault("ERROR: Path: "+ path+" not found. Make sure argument -"+concept+" has correct directory path and user executing FileDestroyer has permissions to read and write.");
                Environment.Exit(-1);
            }
        }

        private static void ExtensionExists(string extension)
        {
            if(extension.Length<1)
            {
                if(logger.Log_path.Length>0)
                {
                    logger.SaveEvent("ERROR: Extension not specified.Make sure argument -ext has been passed.");
                    Environment.Exit(-1);
                }
                else
                {
                    logger = new Logger();
                    logger.SaveDefault("ERROR: Extension not specified.Make sure argument -ext has been passed.");
                    Environment.Exit(-1);
                }
            }
        }

        private static double DaysExists(string days)
        {
            double d = 0;

            if (days.Length > 0)
            {
                if(!double.TryParse(days, out d))
                {
                    logger.SaveEvent("ERROR: Could not read days.Make sure argument -days is a number.");
                    Environment.Exit(-1);
                }
            }
            else
            {
                logger.SaveEvent("ERROR: Number of days not specified.Make sure argument -days has been passed.");
                Environment.Exit(-1);
            }

            return d;
        }
    }
}
