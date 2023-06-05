using Avalonia.Controls;
using Avalonia.Interactivity;
using Material.Demo.Models;

namespace Material.Demo.Pages {
    public partial class SideSheetDemo : UserControl {
        public SideSheetDemo() {
            this.InitializeComponent();
        }

        private void CloseSideInfoButton_OnClick(object? sender, RoutedEventArgs e) {
            var vm = DataContext as SideSheetDemoViewModel;
            if (vm == null)
                return;

            vm.SideInfoOpened = false;
        }

        private void OpenSideInfoButton_OnClick(object? sender, RoutedEventArgs e) {
            var vm = DataContext as SideSheetDemoViewModel;
            if (vm == null)
                return;

            vm.SideInfoOpened = true;
        }
    }
}
