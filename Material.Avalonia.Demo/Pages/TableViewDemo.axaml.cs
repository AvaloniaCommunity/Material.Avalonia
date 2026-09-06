using Avalonia.Controls;
using Material.Avalonia.Demo.ViewModels;

namespace Material.Avalonia.Demo.Pages;

public partial class TableViewDemo : UserControl {
    public TableViewDemo() {
        InitializeComponent();
        DataContext = new TableViewDemoViewModel();
    }
}