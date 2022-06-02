using Material.Dialog.Commands;

namespace Material.Dialog.ViewModels.Elements
{
    public class ObsoleteDialogButtonViewModel : DialogButtonViewModel
    {
        public ObsoleteDialogButtonViewModel(DialogWindowViewModel parent, object content, string result) : base(parent, content)
        {
            _result = result;
            Command = new MaterialDialogRelayCommand(OnExecuteCommandHandler, CanExecuteCommandHandler);
        }

        private bool CanExecuteCommandHandler(object arg)
        {
            return true;
        }

        private void OnExecuteCommandHandler(object obj)
        {
            if (Parent is null)
                return;

            if (obj is ObsoleteDialogButtonViewModel vm)
            {
                Parent.DialogResult = new DialogResult(vm.Result);
            }

            Parent.CloseWindow();
        }

        private string _result;

        /// <summary>
        /// This property is used for compat deprecated dialog library.
        /// </summary>
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }
    }
}