using Material.Dialog.Icons;

namespace Material.Dialog.ViewModels.Elements.Header.Icons
{
    public class DialogIconViewModel : IconViewModelBase
    {
        private DialogIconKind _kind;

        public DialogIconKind Kind
        {
            get => _kind;
            set
            {
                _kind = value;
                OnPropertyChanged();
            }
        }
    }
}