using Requester.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using static Requester.Services.Enums;

namespace Requester.Models
{
    public class RequestObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Request Request { get; set; }

        private HttpWebRequest currentRequest { get; set; }

        public Status Status { get; set; }

        public bool IsAborted { get; private set; } = true;

        public int DurationRequestTime { get; set; }

        public int LastRequestTimeEnded { get; set; }


        public RequestObject()
        {
            Request = new Request("http://httpstat.us/200");
        }

        public RequestObject(Request request)
        {
            this.Request = request;
        }




        public async void Run(bool restart = false)
        {
            if (restart) IsAborted = false;

            if (Status.Equals(Status.ReadyToPost)  )
            {
                if (CheckUrl(this.Request.Url))
                {
                    if (!IsAborted)
                    {
                        DurationRequestTime = 0;
                        RequestAsync(this.Request.Url);
                    }
                }
                else
                {
                    this.Request.Response = "Недопустимый Url";
                }
            }

        }

        public bool CheckUrl(object value)
        {
            Uri uriResult;
            return Uri.TryCreate(value as string, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }




        private void RequestAsync(string url)
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

        public void Abort()
        {
            IsAborted = true;

            if (currentRequest != null)
            {
                currentRequest.Abort();
            }
       
        }




    }
}

