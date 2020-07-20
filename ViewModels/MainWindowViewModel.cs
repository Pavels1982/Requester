using System;
using System.Collections.Generic;
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

        #region Commands
        public ICommand CloseWindow
        {
            get 
            {
                return new RelayCommand((o) => CloseApp());
            }
        }
        #endregion


        #region Constructor
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public MainWindowViewModel()
        { 
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
