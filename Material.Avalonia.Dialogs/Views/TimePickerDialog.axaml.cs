using System;
using Avalonia.Controls;
using Material.Dialog.Interfaces;
using Material.Dialog.ViewModels;

namespace Material.Dialog.Views {
    [Obsolete("Currently unsupported - https://github.com/AvaloniaCommunity/Material.Avalonia/issues/470")]
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