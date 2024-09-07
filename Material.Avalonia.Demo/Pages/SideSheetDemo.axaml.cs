using Avalonia.Controls;
using Avalonia.Interactivity;
using Material.Avalonia.Demo.Models;

namespace Material.Avalonia.Demo.Pages;

public partial class SideSheetDemo : UserControl {
    public SideSheetDemo() {
        InitializeComponent();
    }

    private void OpenSideInfoButton_OnClick(object? sender, RoutedEventArgs e) {
        var vm = DataContext as SideSheetDemoViewModel;
        if (vm == null)
            return;

        vm.SideInfoOpened = true;
    }
}