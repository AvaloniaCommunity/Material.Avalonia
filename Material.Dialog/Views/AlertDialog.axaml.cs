using Avalonia.Controls;
using Material.Dialog.Interfaces;
using Material.Dialog.ViewModels;

namespace Material.Dialog.Views {
    public partial class AlertDialog : Window, IDialogWindowResult<DialogResult>, IHasNegativeResult {
        public AlertDialog() {
            InitializeComponent();
        }

        public DialogResult GetResult() => (DataContext as AlertDialogViewModel)?.DialogResult;

        public void SetNegativeResult(DialogResult result) {
            if (DataContext is AlertDialogViewModel viewModel)
                viewModel.DialogResult = result;
        }
    }
}
