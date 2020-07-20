using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Requester.Services.Enums;

namespace Requester.Models
{
    public class RequestObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Url { get; set; } = "http://httpstat.us/200";
        public string ResponseString { get; set; }
        public int TimeOut { get; set; } = 230;
        private HttpWebRequest currentRequest { get; set; }
        private Status status { get; set; }


        public async void Run()
        {
            if (status.Equals(Status.ReadyToPost) && Check()) await RequestAsync(Url);
        }

        private bool Check() => Url != String.Empty ? true : false;



        private async Task RequestAsync(string url)
        {
            status = Status.Process;

            currentRequest = (HttpWebRequest)WebRequest.Create(url);
            currentRequest.Method = "GET";
            currentRequest.Timeout = this.TimeOut * 1000;
            currentRequest.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)await currentRequest.GetResponseAsync();
            this.ResponseString = response.StatusCode.ToString();
            //using (Stream stream = response.GetResponseStream())
            //{
            //    using (StreamReader reader = new StreamReader(stream))
            //    {
            //        this.ResponseString = reader.ReadToEnd();
            //    }
            //}
            response.Close();
            status = Status.ReadyToPost;
            currentRequest = null;
        }

        public void Abort() => currentRequest.Abort();




    }
}

