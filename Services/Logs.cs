using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requester.Services
{
    /// <summary>
    /// Класс записи логов в файл.
    /// </summary>
    public static class Logs
    {
        /// <summary>
        /// Метод добаления данных в файл логов.
        /// </summary>
        /// <param name="str"></param>
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
