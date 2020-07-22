using MaterialDesignThemes.Wpf;
using Requester.Models;
using Requester.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Requester.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {


        #region Events
        // <summary>
        /// Имплементация интерфейса INotifyPropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets ссылка на синглтон RequestManager.
        /// </summary>
        public RequestManager RequestManager { get; set; } = RequestManager.GetInstance();

        /// <summary>
        /// Gets or sets коллекция запросов.
        /// </summary>
        public ObservableCollection<RequestObject> RequestCollection { get; set; }
        #endregion

        #region Commands
        /// <summary>
        /// Gets создание нового запроса с парамертами по умолчанию, и добавление его в коллекцию запросов.
        /// </summary>
        public ICommand AddNewRequest
        {
            get
            {
                return new RelayCommand((o) => this.RequestManager.Add());
            }
        }

        /// <summary>
        /// Gets запуск запроса на исполнение.
        /// </summary>
        public ICommand RunRequest
        {
            get
            {
                return new RelayCommand<RequestObject>(this.RequestManager.Run);
            }
        }

        /// <summary>
        /// Gets запуск всех прерванных запросов.
        /// </summary>
        public ICommand RunAbortedRequests
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    this.RequestCollection.Where(req => req.IsAborted).ToList().ForEach(request => request.Run(true));
                });
            }

        }

        /// <summary>
        /// Gets прерывание всех запросов.
        /// </summary>
        public ICommand AbortedRequests
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    this.RequestCollection.ToList().ForEach(request => request.Abort());
                });
            }

        }

        /// <summary>
        /// Gets удаление запроса.
        /// </summary>
        public ICommand DeleteRequest
        {
            get
            {
                return new RelayCommand<RequestObject>(ConfirmDelete);
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public MainWindowViewModel()
        {
            this.RequestCollection = this.RequestManager.RequestCollection;
        }
        #endregion

        #region Methods

        private async void ConfirmDelete(RequestObject obj)
        {
            if (await WindowManager.ShowDialog("ResponseDeleteConfirm"))
            {
                this.RequestManager.Remove(obj);
            }
        }

        /// <summary>
        /// Логика выполняемая перед закрытием приложения.
        /// </summary>
        public void BeforeCloseApp()
        {
            XMLHelper.SaveRequests(this.RequestCollection, PathManager.GetConfigFilePath());
        }
        #endregion

    }
}
