using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Material.Dialog.Interfaces;
using Material.Dialog.ViewModels;

namespace Material.Dialog.Views {
    public partial class TimePickerDialog : Window, IDialogWindowResult<DateTimePickerDialogResult>, IHasNegativeResult {
        public TimePickerDialog() {
            Result = new DateTimePickerDialogResult();

            InitializeComponent();

#if DEBUG

            this.AttachDevTools();

#endif
        }
        public DateTimePickerDialogResult Result { get; set; }

        public DateTimePickerDialogResult GetResult() => Result;

        public void SetNegativeResult(DialogResult result) => Result.Result = result.GetResult;

        public void AttachViewModel(TimePickerDialogViewModel vm) {
            this.DataContext = vm;
        }

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
