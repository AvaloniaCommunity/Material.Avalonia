using System;
using Avalonia.Threading;
using Material.Dialog.Commands;
using Material.Dialog.Views;

namespace Material.Dialog.ViewModels
{
    // TODO Fix DatePicker TimePicker dialogs https://github.com/AvaloniaCommunity/Material.Avalonia/issues/470
    [Obsolete("Currently unsupported - https://github.com/AvaloniaCommunity/Material.Avalonia/issues/470")]
    public class DatePickerDialogViewModel : DialogWindowViewModel
    {
        private readonly DatePickerDialog _window;

        public DialogButton PositiveButton { get; internal set; }

        public DialogButton NegativeButton { get; internal set; }

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

        public DatePickerDialogViewModel(DatePickerDialog dialog) : base(dialog) {
            _window = dialog;
            ButtonClick = new MaterialDialogRelayCommand(OnPressButton, CanPressButton);
        }

        public bool CanPressButton(object? args)
        {
            return true;
        }

        public async void OnPressButton(object? args)
        {
            if (args is not DialogButton button)
                return;

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                var result = new DateTimePickerDialogResult(button.Result, DateTime);

                _window.Result = result;
                _window.Close();
            });
        }

        public MaterialDialogRelayCommand ButtonClick { get; }
    }
}