using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Threading;
using Material.Dialog.Interfaces;
using Material.Dialog.ViewModels.Elements;
using Material.Dialog.ViewModels.Elements.Header.Icons;

namespace Material.Dialog.ViewModels
{
    public abstract class DialogWindowViewModel : DialogViewModelBase
    {
        // TODO: Use other way to handle result holder and close window if possible!!!!
        // do not let view model relate with window
        // probably property and simple handler and its enough!!!!!
        public DialogWindowViewModel(IDialogModalHost host)
        {
            _host = host;
        }

        private readonly IDialogModalHost _host;

        protected IDialogModalHost Host => _host;

        #region Base Properties

        private string _windowTitle;

        public string WindowTitle
        {
            get => _windowTitle;
            set
            {
                _windowTitle = value;
                OnPropertyChanged();
            }
        }

        private string _contentHeader;

        public string ContentHeader
        {
            get => _contentHeader;
            set
            {
                _contentHeader = value;
                OnPropertyChanged();
            }
        }

        private string _contentMessage;

        public string ContentMessage
        {
            get => _contentMessage;
            set
            {
                _contentMessage = value;
                OnPropertyChanged();
            }
        }

        private bool _borderless;

        public bool Borderless
        {
            get => _borderless;
            set
            {
                _borderless = value;
                OnPropertyChanged();
            }
        }

        private double? _maxWidth;

        public double? MaxWidth
        {
            get => _maxWidth;
            set
            {
                _maxWidth = value;
                OnPropertyChanged();
            }
        }

        private double? _width;

        public double? Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }

        private WindowStartupLocation _windowStartupLocation;

        public WindowStartupLocation WindowStartupLocation
        {
            get => _windowStartupLocation;
            set
            {
                _windowStartupLocation = value;
                OnPropertyChanged();
            }
        }

        private IconViewModelBase? _dialogIcon;

        public IconViewModelBase? DialogIcon
        {
            get => _dialogIcon;
            set
            {
                _dialogIcon = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private ObservableCollection<DialogButtonViewModel> _altDialogButton;

        public ObservableCollection<DialogButtonViewModel> AltDialogButton
        {
            get => _altDialogButton;
            internal set
            {
                _altDialogButton = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<DialogButtonViewModel> _dialogButtons;

        public ObservableCollection<DialogButtonViewModel> DialogButtons
        {
            get => _dialogButtons;
            internal set
            {
                _dialogButtons = value;
                OnPropertyChanged();
            }
        }

        private Orientation _buttonsStackOrientation;

        public Orientation ButtonsStackOrientation
        {
            get => _buttonsStackOrientation;
            internal set
            {
                _buttonsStackOrientation = value;
                OnPropertyChanged();
            }
        }

        private object? _dialogResult;

        public object? DialogResult
        {
            get => _dialogResult;
            internal set
            {
                _dialogResult = value;
                OnPropertyChanged();
            }
        }

        public async void CloseWindow()
        {
            await Dispatcher.UIThread.InvokeAsync(() => {
                var result = DialogResult;
                if (!_host.OnDialogClosing(result))
                    return;
                
                lock (this) {
                    _host.OnDialogClosed(result);
                }
            });
        }
    }
}
