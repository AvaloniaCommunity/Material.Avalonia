using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Markup.Xaml;
using Material.Dialog.Interfaces;

namespace Material.Dialog.Views
{
    public class CustomDialog : Window, IDialogWindowResult<DialogResult>, IHasNegativeResult
    {
        private ContentPresenter PART_Content;
        
        public DialogResult Result { get; set; } = DialogResult.NoResult;

        public CustomDialog()
        {
            this.InitializeComponent(); 
            
            PART_Content = this.Get<ContentPresenter>(nameof(PART_Content));
        }

        public void ApplyViewModel(object vm) => PART_Content.DataContext = vm;

        public void ApplyContent(object content) => PART_Content.Content = content;

        public DialogResult GetResult() => Result;

        public void SetNegativeResult(DialogResult result) => Result = result;

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
