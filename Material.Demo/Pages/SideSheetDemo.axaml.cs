using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Material.Demo.Models;

namespace Material.Demo.Pages
{
    public class SideSheetDemo : UserControl
    {
        public SideSheetDemo()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void CloseSideInfoButton_OnClick(object? sender, RoutedEventArgs e)
        {
            var vm = DataContext as SideSheetDemoViewModel;
            if (vm == null)
                return;

            vm.SideInfoOpened = false;
        }

        private void OpenSideInfoButton_OnClick(object? sender, RoutedEventArgs e)
        {
            var vm = DataContext as SideSheetDemoViewModel;
            if (vm == null)
                return;

            vm.SideInfoOpened = true;
        }
    }
}
