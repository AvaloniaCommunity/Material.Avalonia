using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Material.Dialog.Interfaces;
using Material.Dialog.ViewModels;

namespace Material.Dialog.Views
{
    public class TimePickerDialog : Window, IDialogWindowResult<DateTimePickerDialogResult>, IHasNegativeResult
    {
        public DateTimePickerDialogResult Result { get; set; }
        
        public TimePickerDialog()
        {
            Result = new DateTimePickerDialogResult();
            
            InitializeComponent();
        }

        public void AttachViewModel(TimePickerDialogViewModel vm)
        {
            this.DataContext = vm;
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
        }

        public DateTimePickerDialogResult GetResult() => Result;
        
        public void SetNegativeResult(DialogResult result) => Result.Result = result.GetResult;
        
        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
