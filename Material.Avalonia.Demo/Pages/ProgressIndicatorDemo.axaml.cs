using Avalonia.Controls;
using Material.Avalonia.Demo.ViewModels;

namespace Material.Avalonia.Demo.Pages;

public partial class ProgressIndicatorDemo : UserControl {
    public ProgressIndicatorDemo() {
        InitializeComponent();

        DataContext = new ProgressIndicatorDemoViewModel();
    }
}