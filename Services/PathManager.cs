using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requester.Services
{
    public static class PathManager
    {
        private static readonly string MainPath = System.AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string ConfigFile = "config.xml";
        private static readonly string XSDFile = "config.xsd";
        private static readonly string LogFile = "Log.txt";

        public static string GetConfigFilePath()
        {
            string file = MainPath + ConfigFile;
            CheckExisting(file);
            return file;
        }

        private static void CheckExisting(string filename)
        {
            if (!File.Exists(filename))
            {
                File.Create(filename).Close();
            }
        }

        public static string GetLogFilePath()
        {
            string file = MainPath + LogFile;
            CheckExisting(file);
            return file;
        }

        public static string GetXSDFilePath()
        {
            string file = MainPath + XSDFile;
            return file;
        }




    }
}
