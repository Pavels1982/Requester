using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requester.CustomControls
{
    public class ConfirmDialogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        /// <summary>
        /// Gets or sets сообщение выводимое в диалоговом окне.
        /// </summary>
        public string Message { get; set; }


        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        /// <param name="message"></param>
        public ConfirmDialogViewModel(string message)
        {
            this.Message = message;
        }

    }
}
