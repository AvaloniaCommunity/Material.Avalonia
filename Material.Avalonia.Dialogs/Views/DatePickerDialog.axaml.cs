using System;
using Avalonia.Controls;
using Material.Dialog.Interfaces;
using Material.Dialog.ViewModels;

namespace Material.Dialog.Views {
    // TODO Fix DatePicker TimePicker dialogs https://github.com/AvaloniaCommunity/Material.Avalonia/issues/470
    [Obsolete("Currently unsupported - https://github.com/AvaloniaCommunity/Material.Avalonia/issues/470")]
    public partial class DatePickerDialog : Window, IDialogWindowResult<DateTimePickerDialogResult>, IHasNegativeResult {
        private DatePickerDialogViewModel viewModel;

        public DatePickerDialog() {
            Result = new DateTimePickerDialogResult();

            InitializeComponent();
        }

        public DateTimePickerDialogResult Result { get; set; }

        public DateTimePickerDialogResult GetResult() {
            return Result;
        }

        public void SetNegativeResult(DialogResult result) {
            Result._result = result.GetResult;
        }

        public void AttachViewModel(DatePickerDialogViewModel vm) {
            DataContext = vm;
            viewModel = vm;
        }
    }
}
