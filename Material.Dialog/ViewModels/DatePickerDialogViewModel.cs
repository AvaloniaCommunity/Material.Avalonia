using System;
using Avalonia.Threading;
using Material.Dialog.Commands;
using Material.Dialog.Views;

namespace Material.Dialog.ViewModels
{
    public class DatePickerDialogViewModel : DialogWindowViewModel
    {
        private DatePickerDialog _window;

        private DialogResultButton _positiveButton;
        public DialogResultButton PositiveButton { get => _positiveButton; internal set => _positiveButton = value; }

        private DialogResultButton _negativeButton;
        public DialogResultButton NegativeButton { get => _negativeButton; internal set => _negativeButton = value; }

        private DateTime _dateTime;
        public DateTime DateTime { get => _dateTime; set { _dateTime = value; OnPropertyChanged(); } }
        
        public DatePickerDialogViewModel(DatePickerDialog dialog)
        {
            _window = dialog;
            ButtonClick = new RelayCommand(OnPressButton, CanPressButton);
        }
        
        public bool CanPressButton(object args) => true;
        public async void OnPressButton(object args)
        {
            var button = args as DialogResultButton;
            if (button is null)
                return; 

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                //_window.Result = result;
                _window.Close();
            });
        }

        public RelayCommand ButtonClick { get; private set; }
    }
}