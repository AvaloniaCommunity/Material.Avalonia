using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Material.Styles.Controls;
using Material.Styles.Models;

namespace Material.Demo.Pages;

public partial class SnackbarDemo : UserControl {
    private int _actionCount;
    
    public SnackbarDemo() {
        InitializeComponent();
    }

    private void SnackbarHost1Button_OnClick(object? sender, RoutedEventArgs e) {
        SnackbarHost.Post(
            new SnackbarModel(
                "Hello from Snackbar.",
                TimeSpan.FromSeconds(8)),
            SnackbarHost1.HostName,
            DispatcherPriority.Normal);
    }

    private void SnackbarHost2Button_OnClick(object? sender, RoutedEventArgs e) {
        SnackbarHost.Post(
            new SnackbarModel(
                "Hello from Snackbar.",
                TimeSpan.FromSeconds(8),
                new SnackbarButtonModel {
                    Text = "Click me",
                    Action = () => {
                        _actionCount++;
                        ActionCount.Text = _actionCount.ToString();
                    }
                }),
            SnackbarHost2.HostName,
            DispatcherPriority.Normal);
    }
}