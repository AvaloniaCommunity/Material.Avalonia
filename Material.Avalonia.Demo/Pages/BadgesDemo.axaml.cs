using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Material.Styles.Controls;
using Material.Styles.Enums;

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
        InteractiveBadged.BadgeContent = _inboxCount.ToString();
    }
}