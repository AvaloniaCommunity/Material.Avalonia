using System.Windows.Input;

namespace Material.Dialog.ViewModels.Elements
{
    public class DialogButtonViewModel : DialogViewModelBase
    {
        internal DialogButtonViewModel(DialogWindowViewModel parent, object content)
        {
            _parent = parent;
            _content = content;
        }
        
        public DialogButtonViewModel(DialogWindowViewModel parent, object content, ICommand command)
        {
            _parent = parent;
            _content = content;
            _command = command;
        }
        
        private DialogWindowViewModel _parent;

        public DialogWindowViewModel Parent => _parent;
        

        private bool _isPositiveButton;
        
        public bool IsPositiveButton
        {
            get => _isPositiveButton;
            set
            {
                _isPositiveButton = value; 
                OnPropertyChanged();
            }
        }

        private object _content;

        public object Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        private ICommand _command;

        public ICommand Command
        {
            get => _command;
            set
            {
                _command = value;
                OnPropertyChanged();
            }
        }
    }
}