using System.ComponentModel;

namespace Requester.Models
{

    public class Request : INotifyPropertyChanged
    {


        public string Url { get; set; }

        public string Response { get; set; }

        public int TimeOut { get; set; }

        public int Interval { get; set; }


        public Request()
        {
            GetDefaultParameter(this);
        }

        public Request(string url, int timeOut, int interval)
        {
            this.Url = url;
            this.TimeOut = timeOut;
            this.Interval = interval;
        }

        private void GetDefaultParameter(Request request)
        {
            this.Url = "http://httpstat.us/200?sleep=5000";
            this.TimeOut = 7;
            this.Interval = 10;
        }




        public event PropertyChangedEventHandler PropertyChanged;
    }
}
