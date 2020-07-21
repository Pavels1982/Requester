using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requester.Services
{
    public static class Logs
    {
        public static void Add(string str)
        {
            string dateTime = DateTime.UtcNow.ToString();
            try
            {
                using (StreamWriter stream = new StreamWriter(PathManager.GetLogFilePath(), true))
                {
                    stream.Write(string.Format("{0} - {1} \n", dateTime, str));
                    stream.Close();
                }
            }
            catch { }


        }
    }
}
