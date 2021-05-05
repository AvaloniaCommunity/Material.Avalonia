using System;
using Avalonia.Threading;
using Material.Dialog.Commands;
using Material.Dialog.Views;

namespace Material.Dialog.ViewModels
{
    public class DatePickerDialogViewModel : DialogWindowViewModel
    {
        private readonly DatePickerDialog _window;

        public DialogResultButton PositiveButton { get; internal set; }

        public DialogResultButton NegativeButton { get; internal set; }

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get => _dateTime;
            set
            {
                _dateTime = value;
                OnPropertyChanged();
            }
        }

        public DatePickerDialogViewModel(DatePickerDialog dialog)
        {
            _window = dialog;
            ButtonClick = new RelayCommand(OnPressButton, CanPressButton);
        }

        public bool CanPressButton(object args)
        {
            return true;
        }
        public async void OnPressButton(object args)
        {
            var button = args as DialogResultButton;
            if (button is null)
                return;

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                var result = new DateTimePickerDialogResult(button.Result, DateTime);

                _window.Result = result;
                _window.Close();
            });
        }

        public RelayCommand ButtonClick { get; }
    }
}