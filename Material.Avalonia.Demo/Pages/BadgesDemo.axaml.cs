using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Material.Styles.Controls;

namespace Material.Avalonia.Demo.Pages;

public partial class BadgesDemo : UserControl {
    private int _inboxCount;

    public BadgesDemo() {
        InitializeComponent();
        badgePlacementSelector.ItemsSource = Enum.GetValues<BadgePlacement>();
        badgePlacementSelector.SelectedItem = BadgePlacement.TopRight;
    }

    private void OnIncrementBadgeClick(object? sender, RoutedEventArgs e) {
        _inboxCount++;
        interactiveBadged.BadgeContent = _inboxCount.ToString();
    }
}

