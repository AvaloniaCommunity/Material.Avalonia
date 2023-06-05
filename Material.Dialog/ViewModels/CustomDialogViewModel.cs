using Avalonia.Controls.Templates;
using Material.Dialog.Views;

namespace Material.Dialog.ViewModels {
    public class CustomDialogViewModel : DialogWindowViewModel {
        private object _content;

        public object Content {
            get => _content;
            set {
                _content = value;
                OnPropertyChanged();
            }
        }

        private IDataTemplate _contentTemplate;

        public IDataTemplate ContentTemplate {
            get => _contentTemplate;
            set {
                _contentTemplate = value;
                OnPropertyChanged();
            }
        }

        public CustomDialogViewModel(CustomDialog dialog) : base(dialog) {
        }
    }
}
