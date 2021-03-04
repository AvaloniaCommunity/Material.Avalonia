using Avalonia.Layout;
using Avalonia.Threading;
using Material.Dialog.Views;

namespace Material.Dialog.ViewModels
{
    public class CustomDialogViewModel : DialogWindowViewModel
    {
        private CustomDialog _window;

        private object _content;

        public object Content {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        private DialogResultButton[] _dialogButtons;
        public DialogResultButton[] DialogButtons { get => _dialogButtons; internal set => _dialogButtons = value; }

        private Orientation _buttonsStackOrientation;
        public Orientation ButtonsStackOrientation { get => _buttonsStackOrientation; internal set => _buttonsStackOrientation = value; }

        public CustomDialogViewModel(CustomDialog dialog)
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