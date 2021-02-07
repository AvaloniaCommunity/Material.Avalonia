using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Material.Dialog.Interfaces;

namespace Material.Dialog.Views
{
    public class TimePickerDialog : Window, IDialogWindowResult<DateTimePickerDialogResult>
    {
        public DateTimePickerDialogResult Result { get; set; }
        
        public TimePickerDialog()
        {
            Result = new DateTimePickerDialogResult();
            
            InitializeComponent();
        }
        
        public DateTimePickerDialogResult GetResult() => Result;
        
        public void SetNegativeResult(DialogResult result) => Result._result = result.GetResult;
        
        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
