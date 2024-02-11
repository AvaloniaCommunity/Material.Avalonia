using Avalonia.Controls;
using Material.Avalonia.Demo.ViewModels;

namespace Material.Avalonia.Demo.Pages;

public partial class FieldsDemo : UserControl {
    public FieldsDemo() {
        InitializeComponent();

        DataContext = new TextFieldsViewModel();
    }
}