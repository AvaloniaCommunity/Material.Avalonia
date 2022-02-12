using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Material.Dialog.Interfaces;
using Material.Dialog.ViewModels;

namespace Material.Dialog.Views
{
    public class CustomDialog : Window, IDialogWindowResult<DialogResult>, IHasNegativeResult
    {
        public CustomDialog()
        {
            InitializeComponent(); 
        }

        public DialogResult GetResult() => (DataContext as CustomDialogViewModel)?.DialogResult;

        public void SetNegativeResult(DialogResult result)
        {
            if (DataContext is CustomDialogViewModel viewModel)
                viewModel.DialogResult = result;
        }

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
