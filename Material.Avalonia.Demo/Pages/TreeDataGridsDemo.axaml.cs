using Avalonia.Controls;
using Material.Avalonia.Demo.ViewModels;

namespace Material.Avalonia.Demo.Pages;

public partial class TreeDataGridsDemo : UserControl {
    public TreeDataGridsDemo() {
        InitializeComponent();

        DataContext = new TreeDataGridsDemoViewModel();
    }
}
