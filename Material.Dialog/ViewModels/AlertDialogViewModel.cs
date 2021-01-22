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

        private DialogResultButton[] m_DialogButtons;
        public DialogResultButton[] DialogButtons { get => m_DialogButtons; internal set => m_DialogButtons = value; }

        private Orientation m_ButtonsStackOrientation;
        public Orientation ButtonsStackOrientation { get => m_ButtonsStackOrientation; internal set => m_ButtonsStackOrientation = value; }

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
