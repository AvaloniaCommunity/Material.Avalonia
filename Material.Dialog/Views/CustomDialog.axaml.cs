using Avalonia.Controls;
using Material.Dialog.Interfaces;
using Material.Dialog.ViewModels;

namespace Material.Dialog.Views {
    public partial class CustomDialog : Window, IDialogWindowResult<DialogResult>, IHasNegativeResult {
        public CustomDialog() {
            InitializeComponent();
        }

        public DialogResult GetResult() {
            return (DataContext as CustomDialogViewModel)?.DialogResult;
        }

        public void SetNegativeResult(DialogResult result) {
            if (DataContext is CustomDialogViewModel viewModel)
                viewModel.DialogResult = result;
        }
    }
}
