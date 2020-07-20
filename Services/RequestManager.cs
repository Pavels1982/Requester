using Requester.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Requester.Services
{
    public class RequestManager : INotifyPropertyChanged
    {
        public ObservableCollection<RequestObject> RequestCollection { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private static  RequestManager instance;

        public static RequestManager GetInstance()
        {
            if (instance == null)
            {
                instance = new RequestManager();
                instance.RequestCollection = new ObservableCollection<RequestObject>();
            }

            return instance;
        }

      
        public void Run(RequestObject selectedRequest)
        {
            selectedRequest.Run();
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
