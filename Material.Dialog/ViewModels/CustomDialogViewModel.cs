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