using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requester.Models
{
    public class Request : INotifyPropertyChanged
    {
        public string Url { get; set; }

        public string Response { get; set; }

        public int TimeOut { get; set; }

        public int Interval { get; set; }


        public Request(string url, int timeOut = 5)
        {
            this.Url = url;
            this.TimeOut = timeOut;
        }

        public Request(string url, int timeOut, int interval)
        {
            this.Url = url;
            this.TimeOut = timeOut;
            this.Interval = interval;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
