﻿using Requester.Services;
using Requester.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Requester.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        #region Fields
        /// <summary>
        /// Контекст синхронизации.
        /// </summary>
        private SynchronizationContext current = SynchronizationContext.Current;
        #endregion

        public MainWindowView()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            bool result = await WindowManager.ShowDialog("CloseAppConfirm");
            if (result)
            {
                (this.DataContext as MainWindowViewModel).BeforeCloseApp();
                e.Cancel = false;
                Application.Current.Shutdown();
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            new Thread((o) =>
            {
                while (XMLHelper.ValidationStatus.Equals(Enums.ValidationStatus.Process)) { }

                if (XMLHelper.ValidationStatus.Equals(Enums.ValidationStatus.Denied)) this.current.Post(ShowConfirmDialog, "ValidationError");
            }).Start();
        }


        /// <summary>
        /// Отображение диалогового окна.
        /// </summary>
        /// <param name="msg"></param>
        private async void ShowConfirmDialog(object msg)
        {
            if (!await WindowManager.ShowDialog(msg as string))
            {
                Application.Current.Shutdown();
            }

        }
    }
}
