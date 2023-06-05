using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using DialogHostAvalonia;
using Material.Demo.Models;
using Material.Demo.ViewModels;

namespace Material.Demo.Pages {
    public partial class DialogDemo : UserControl {
        public DialogDemo() {
            InitializeComponent();
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e) {
            // Lazy Initialize view model
            DataContext ??= new DialogDemoViewModel();

            base.OnApplyTemplate(e);
        }

        private void OpenDialogWithView(object? sender, RoutedEventArgs e) {
            DialogHost.Show(Resources["Sample2View"]!, "MainDialogHost");
        }

        private void OpenDialogWithModel(object? sender, RoutedEventArgs e) {
            // View that associated with this model defined at DialogContentTemplate in DialogDemo.axaml
            DialogHost.Show(new Sample2Model(new Random().Next(0, 100)), "MainDialogHost");
        }

        private void OpenMoreDialogHostExamples(object? sender, RoutedEventArgs e) {
            Process.Start(new ProcessStartInfo()
                { FileName = "https://github.com/AvaloniaUtils/DialogHost.Avalonia", UseShellExecute = true });
        }
    }
}
