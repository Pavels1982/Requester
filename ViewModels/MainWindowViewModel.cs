using Requester.Models;
using Requester.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Requester.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RequestManager RequestManager { get; set; } = RequestManager.GetInstance();
        public ObservableCollection<RequestObject> RequestCollection { get; set; } 

        #region Commands
        public ICommand CloseWindow
        {
            get
            {
                return new RelayCommand((o) => CloseApp());
            }
        }

        public ICommand AddNewRequest
        {
            get
            {
                return new RelayCommand((o) => RequestManager.Add());
            }
        }

        public ICommand RunRequest
        {
            get
            {
                return new RelayCommand<RequestObject>(RequestManager.Run);
            }

        }

        #endregion


        #region Constructor
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public MainWindowViewModel()
        {
            this.RequestCollection = RequestManager.RequestCollection;
        }
        #endregion


        #region Methods
        private void CloseApp()
        {
            Application.Current.Shutdown();
        }
        #endregion

    }
}
