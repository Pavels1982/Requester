using Requester.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using static Requester.Services.Enums;

namespace Requester.Services
{
    public class RequestManager : INotifyPropertyChanged
    {
        #region Fields
        /// <summary>
        /// Имплементация паттерна Singlton. Представляет единичный экземпляр класса <see cref="RequestManager"/>.
        /// </summary>
        private static RequestManager instance;
       
        /// <summary>
        ///  Таймер проверяет коллекцию запросов на наличие запросов с интервалом запуска.  
        /// </summary>
        private DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        /// <summary>
        /// Gets or sets коллекция запросов.
        /// </summary>
        public ObservableCollection<RequestObject> RequestCollection { get; private set; }

        /// <summary>
        /// Gets or sets количество прерванный либо невыполняемых в данный момент запросов.
        /// </summary>
        public int AbortedRequests { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Метод для получения объекта RequestManager (реализация синглтона).
        /// </summary>
        /// <returns></returns>
        public static RequestManager GetInstance()
        {
            if (instance == null)
            {
                instance = new RequestManager();
                if (XMLHelper.ValidateXML(PathManager.GetConfigFilePath(), PathManager.GetXSDFilePath()).Equals(ValidationStatus.Passed))
                    instance.RequestCollection = XMLHelper.LoadRequests(PathManager.GetConfigFilePath());
                else
                    instance.RequestCollection = new ObservableCollection<RequestObject>();

                instance.timer.Tick += instance.Timer_Tick;
                instance.timer.Start();
            }

            return instance;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int temp = 0;
            foreach (var request in instance.RequestCollection)
            {
                if (request.LastRequestTimeEnded > 0 && !request.IsAborted && request.Request.Interval > 0)
                {
                    if ((UnixTimeHelper.UnixTimeNow() - request.LastRequestTimeEnded) >= request.Request.Interval)
                    {
                        if (request.Status.Equals(Status.ReadyToPost))
                            request.Run();
                    }
                }

                if (request.Status.Equals(Status.ReadyToPost) && request.IsAborted)
                    temp++;
            }
            if (AbortedRequests != temp) AbortedRequests = temp;
        }


        /// <summary>
        /// Метод запускающий процесс запроса.
        /// </summary>
        /// <param name="selectedRequest"></param>
        public void Run(RequestObject selectedRequest)
        {
            if (selectedRequest.Status.Equals(Status.ReadyToPost))
            {
                 selectedRequest.Run(true);
            }
            else
            {
                selectedRequest.Abort();
            }
        }

        /// <summary>
        /// Метод добавления нового объекта запроса в коллекцию запросов, с параметрами по умолчанию.
        /// </summary>
        public void Add()
        {
            instance.RequestCollection.Add(new RequestObject());
            Logs.Add(String.Format("Добавление пользователем нового запроса. "));
        }

        /// <summary>
        /// Метод удаления объекта запроса из коллекции запросов.
        /// </summary>
        /// <param name="request"></param>
        public void Remove(RequestObject request)
        {
            Logs.Add(String.Format(string.Format("Удаление пользователем запроса с параметрами: Url {0}, TimeOut {1}, Interval {2}", request.Request.Url, request.Request.TimeOut, request.Request.Interval)));
            instance.RequestCollection.Remove(request);

        }
        #endregion





    }
}
