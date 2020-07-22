using Requester.Services;
using System;
using System.ComponentModel;
using System.Net;
using System.Threading;
using static Requester.Services.Enums;

namespace Requester.Models
{
    /// <summary>
    /// Класс объекта модели запроса.
    /// </summary>
    public class RequestObject : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        #region Field
        /// <summary>
        /// Текущий запрос. 
        /// </summary>
        private HttpWebRequest currentRequest;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets модель запроса.
        /// </summary>
        public Request Request { get; set; }

        /// <summary>
        /// Gets or sets статус запроса.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Get or set a value indicating whether указывает прерван ли запрос.
        /// </summary>
        public bool IsAborted { get; private set; } = true;

        /// <summary>
        /// Gets or sets время затраченое на выполнение запроса.
        /// </summary>
        public int DurationRequestTime { get; private set; }

        /// <summary>
        /// Gets or sets время поледнего обращения к серверу.
        /// </summary>
        public int LastRequestTimeEnded { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public RequestObject()
        {
            Request = new Request();
        }

        /// <summary>
        /// Конструктор, где в качестве параметра передаётся экземпляр класса Request.
        /// </summary>
        public RequestObject(Request request)
        {
            this.Request = request;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Метод предварительной подготовки к запуску процесса запроса.
        /// </summary>
        /// <param name="restart"></param>
        public void Run(bool restart = false)
        {
            if (restart) IsAborted = false;

            if (Status.Equals(Status.ReadyToPost)  )
            {
                if (CheckUrl(this.Request.Url))
                {
                    if (!IsAborted)
                    {
                        DurationRequestTime = 0;
                        StartRequest(this.Request.Url);
                    }
                }
                else
                {
                    this.Request.Response = "Недопустимый Url";
                }
            }

        }
        

        /// <summary>
        /// Метод проверки валидности Url.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CheckUrl(object value)
        {
            Uri uriResult;
            return Uri.TryCreate(value as string, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }



        /// <summary>
        /// Метод запускающий новый поток с запросом по указанному адресу. 
        /// </summary>
        /// <param name="url">Адрес сервера</param>
        private void StartRequest(string url)
        {
            Status = Status.Process;

             new Thread((o) =>
             {
                 currentRequest = (HttpWebRequest)WebRequest.Create(url);
                 //currentRequest.Method = "GET";
                 currentRequest.Timeout = this.Request.TimeOut * 1000;
                 currentRequest.ContentType = "application/json";
                 this.Request.Response = string.Empty;

                 this.LastRequestTimeEnded = UnixTimeHelper.UnixTimeNow();
                 try
                 {
                    HttpWebResponse response = (HttpWebResponse)currentRequest.GetResponse();
                    this.Request.Response = response.StatusCode.ToString();
                    response.Close();
                 }
                catch (Exception ex)
                {
                    this.Request.Response = ex.ToString();
                }

                this.DurationRequestTime = UnixTimeHelper.UnixTimeNow() - this.LastRequestTimeEnded;
                Status = Status.ReadyToPost;

                if (this.Request.Interval == 0) IsAborted = true;

                 string logString = string.Format("Адрес {0} вернул код: {1}. Время обращения: {2}. Ожидание ответа: {3}", Request.Url, Request.Response, TimeToString(LastRequestTimeEnded), TimeToString(DurationRequestTime));
                 Logs.Add(logString);
                 currentRequest = null;
            }).Start();
        }

        private string TimeToString(int seconds)
        {
            var t = TimeSpan.FromSeconds(seconds);
            return t.ToString(@"hh\:mm\:ss");
        }

        /// <summary>
        /// Метод прерывание запроса.
        /// </summary>
        public void Abort()
        {
            IsAborted = true;

            if (currentRequest != null)
            {
                currentRequest.Abort();
            }
       
        }
        #endregion

    }
}

