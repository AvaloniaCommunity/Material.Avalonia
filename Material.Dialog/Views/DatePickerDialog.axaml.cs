using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Material.Dialog.Interfaces;
using Material.Dialog.ViewModels;

namespace Material.Dialog.Views
{
    [Obsolete("This feature is still not ready for use! Please come back later!")]
    public class DatePickerDialog : Window, IDialogWindowResult<DateTimePickerDialogResult>, IHasNegativeResult
    {
        private DatePickerDialogViewModel viewModel;
        
        public DateTimePickerDialogResult Result { get; set; }
        
        public DatePickerDialog()
        {
            Result = new DateTimePickerDialogResult();
            
            InitializeComponent();
            // Create decorations
        }

        public void AttachViewModel(DatePickerDialogViewModel vm)
        {
            this.DataContext = vm;
            viewModel = vm;
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            
            //PART_PagesRoot = this.Get<Carousel>("PART_PagesRoot");
        }

        public DateTimePickerDialogResult GetResult() => Result;
        
        public void SetNegativeResult(DialogResult result) => Result.Result = result.GetResult;
        
        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
