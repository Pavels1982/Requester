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

        public static string GetConfigFilePath()
        {
            string file = MainPath + ConfigFile;
            if (!File.Exists(file))
            {
                File.Create(file).Close();
            }
            return file;
        }




    }
}
