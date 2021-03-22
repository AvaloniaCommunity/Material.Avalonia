using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Material.Dialog.Interfaces;

namespace Material.Dialog.Views
{
    public class CustomDialog : Window, IDialogWindowResult<DialogResult>, IHasNegativeResult
    { 
        public DialogResult Result { get; set; } = DialogResult.NoResult;

        public CustomDialog()
        {
            this.InitializeComponent(); 
        }
        
        public DialogResult GetResult() => Result;

        public void SetNegativeResult(DialogResult result) => Result = result;

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
