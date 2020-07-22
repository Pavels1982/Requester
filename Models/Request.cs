using System.ComponentModel;

namespace Requester.Models
{
    /// <summary>
    /// Класс модели запроса.
    /// </summary>
    public class Request : INotifyPropertyChanged
    {

        #region Properties
        /// <summary>
        /// Gets or sets адрес сервера.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets строка ответа от сервера.
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Gets or sets максимальное время ожидания от сервера в сек.
        /// </summary>
        public int TimeOut { get; set; }

        /// <summary>
        /// Gets or sets интервал между запросами в сек.
        /// </summary>
        public int Interval { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        ///  Конструктор по умолчанию.
        /// </summary>
        public Request()
        {
            this.Url = "http://httpstat.us/200?sleep=5000";
            this.TimeOut = 7;
            this.Interval = 10;
        }

        /// <summary>
        /// Конструктор для создания экзепляра класса по входным параметрам.
        /// </summary>
        /// <param name="url">Адрес сервера.</param>
        /// <param name="timeOut">Максимальное время ожидания от сервера в сек.</param>
        /// <param name="interval">Интервал между запросами в сек.</param>
        public Request(string url, int timeOut, int interval)
        {
            this.Url = url;
            this.TimeOut = timeOut;
            this.Interval = interval;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
