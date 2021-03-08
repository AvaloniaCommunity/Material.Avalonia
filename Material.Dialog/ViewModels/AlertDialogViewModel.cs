using Avalonia.Layout;
using Avalonia.Threading;
using Material.Dialog.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Material.Dialog.ViewModels
{
    public class AlertDialogViewModel : DialogWindowViewModel
    {
        private AlertDialog _window;


        public AlertDialogViewModel(AlertDialog dialog)
        {
            _window = dialog;
        }

        public async void ButtonClick (string param)
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                _window.Result = new DialogResult(param);
                _window.Close();
            });
        }
    }
}
