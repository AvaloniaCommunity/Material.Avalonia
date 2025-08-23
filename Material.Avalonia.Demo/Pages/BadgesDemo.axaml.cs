using System;
using Avalonia.Controls;
using Material.Styles.Controls;

namespace Material.Avalonia.Demo.Pages;

public partial class BadgesDemo : UserControl {
    public BadgesDemo() {
        InitializeComponent();
        badgePlacementSelector.ItemsSource = Enum.GetValues<BadgePlacement>();
        badgePlacementSelector.SelectedItem = BadgePlacement.TopRight;
    }
}

