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
        public ObservableCollection<RequestObject> RequestCollection { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private DispatcherTimer timer { get; set; } = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };

        private static  RequestManager instance;

        public int AbortedRequests { get; set; }

        public static RequestManager GetInstance()
        {
            if (instance == null)
            {
                instance = new RequestManager();
                instance.RequestCollection = XMLHelper.LoadRequests(PathManager.GetConfigFilePath());
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

        public void Add()
        {
            instance.RequestCollection.Add(new RequestObject());
        }

        public void Remove(RequestObject request)
        {
            instance.RequestCollection.Remove(request);
        }

        




    }
}
