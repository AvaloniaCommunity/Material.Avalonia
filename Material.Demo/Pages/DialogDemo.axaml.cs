using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Material.Demo.Models;
using Material.Demo.ViewModels;
using Material.Dialog;

namespace Material.Demo.Pages
{
    public class DialogDemo : UserControl
    {


        public DialogDemo()
        {
            this.InitializeComponent();

            DataContext = new DialogDemoViewModel();
        } 

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OpenDialogWithView(object? sender, RoutedEventArgs e) {
            DialogHost.DialogHost.Show(this.Resources["Sample2View"]!, "MainDialogHost");
        }

        private void OpenDialogWithModel(object? sender, RoutedEventArgs e) {
            // View that associated with this model defined at DialogContentTemplate in DialogDemo.axaml
            DialogHost.DialogHost.Show(new Sample2Model(new Random().Next(0, 100)), "MainDialogHost");
        }

        private void OpenMoreDialogHostExamples(object? sender, RoutedEventArgs e) {
            Process.Start("https://github.com/AvaloniaUtils/DialogHost.Avalonia");
        }
    }
}
