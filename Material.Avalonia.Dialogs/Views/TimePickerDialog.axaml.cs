using Avalonia.Controls;
using Material.Dialog.Interfaces;
using Material.Dialog.ViewModels;

namespace Material.Dialog.Views {
    public partial class TimePickerDialog : Window, IDialogWindowResult<DateTimePickerDialogResult>, IHasNegativeResult {
        public TimePickerDialog() {
            Result = new DateTimePickerDialogResult();

            InitializeComponent();
        }
        public DateTimePickerDialogResult Result { get; set; }

        public DateTimePickerDialogResult GetResult() => Result;

        public void SetNegativeResult(DialogResult result) => Result._result = result.GetResult;

        public void AttachViewModel(TimePickerDialogViewModel vm) {
            DataContext = vm;
        }
    }
}