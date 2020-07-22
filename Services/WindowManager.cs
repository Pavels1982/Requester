using MaterialDesignThemes.Wpf;
using Requester.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Requester.Services
{
    public static class WindowManager
    {
        #region Methods
        /// <summary>
        /// Отображение диалогового окна. 
        /// </summary>
        /// <param name="message"></param>
        /// <returns>true - Нажата кнопка ОК, false - Нажата кнопка Отмена</returns>
        public static async Task<bool> ShowDialog(string message)
        {
            string msg = (string)Application.Current.Resources[message];

            if (msg == null) msg = message;

            var view = new ConfirmDialog()
            {
                DataContext = new ConfirmDialogViewModel(msg)
            };

            return (bool)await DialogHost.Show(view);
        }
        #endregion

    }
}
