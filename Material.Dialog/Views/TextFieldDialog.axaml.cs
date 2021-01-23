using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Material.Dialog.Interfaces;
using System;

namespace Material.Dialog.Views
{
    public class TextFieldDialog : Window, IDialogWindowResult<TextFieldDialogResult>
    {
        public TextFieldDialogResult Result { get; set; }

        public TextFieldDialog()
        {
            Result = new TextFieldDialogResult();

            InitializeComponent();
        }
         
        public TextFieldDialogResult GetResult() => Result;

        public void SetNegativeResult(DialogResult result) => Result.result = result.GetResult;

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
