using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Material.Avalonia.Demo.Pages;

public partial class CommandBarDemo : UserControl {
    public CommandBarDemo() {
        InitializeComponent();
    }

    private void OnCommandClick(object? sender, RoutedEventArgs e) {
        var commandName = sender switch {
            CommandBarButton button => button.Label,
            CommandBarToggleButton toggleButton => toggleButton.Label,
            _ => "Command"
        };

        StatusText.Text = $"{commandName} selected.";
    }
}