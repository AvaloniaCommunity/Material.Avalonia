using Avalonia.Layout;
using Avalonia.Threading;
using Material.Dialog.Commands;
using Material.Dialog.ViewModels.TextField;
using Material.Dialog.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Material.Dialog.ViewModels
{
    public class TimePickerDialogViewModel : DialogWindowViewModel
    {
        private TimePickerDialog _window;

        private DialogResultButton[] m_DialogButtons;
        public DialogResultButton[] DialogButtons { get => m_DialogButtons; internal set => m_DialogButtons = value; }

        private DialogResultButton m_PositiveButton;
        public DialogResultButton PositiveButton { get => m_PositiveButton; internal set => m_PositiveButton = value; }

        private DialogResultButton m_NegativeButton;
        public DialogResultButton NegativeButton { get => m_NegativeButton; internal set => m_NegativeButton = value; }

        public TimePickerDialogViewModel(TimePickerDialog dialog)
        {
            _window = dialog;
            ButtonClick = new RelayCommand(OnPressButton, CanPressButton);
        }

        public bool ValidateFields()
        {
            return true;
        }

        public bool CanPressButton(object args) => true;
        public async void OnPressButton(object args)
        {
            var button = args as DialogResultButton;
            if (button is null)
                return; 

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                var result = new DateTimePickerDialogResult() { _result = button.Result };
                var fields = new List<TextFieldResult>();

                result._dateTime = new DateTime();
                _window.Result = result;
                _window.Close();
            });
        }

        public RelayCommand ButtonClick { get; private set; }
    }
}
