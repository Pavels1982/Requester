using Requester.Services;
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
        public MainWindowView()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            bool result = await WindowManager.ShowDialog("Выйти из приложения?");
            if (result)
            {
                (this.DataContext as MainWindowViewModel).BeforeCloseApp();
                e.Cancel = false;
                Application.Current.Shutdown();
            }

        }



        private async void Window_Activated(object sender, EventArgs e)
        {
            if (XMLHelper.ValidationStatus.Equals(Enums.ValidationStatus.Denied))
            {

            }
        }
    }
}
