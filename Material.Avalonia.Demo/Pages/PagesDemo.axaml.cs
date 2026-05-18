using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Material.Avalonia.Demo.Pages;

public partial class PagesDemo : UserControl {
    public PagesDemo() {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e) {
        base.OnLoaded(e);

        var navPage = this.FindControl<NavigationPage>("DemoNavigationPage");
        var pushButton = this.FindControl<Button>("NavPushButton");

        if (pushButton != null && navPage != null) {
            pushButton.Click += async (_, _) => {
                var detailPage = new ContentPage {
                    Header = "Detail Page",
                    Content = new StackPanel {
                        HorizontalAlignment = global::Avalonia.Layout.HorizontalAlignment.Center,
                        VerticalAlignment = global::Avalonia.Layout.VerticalAlignment.Center,
                        Children = {
                            new TextBlock {
                                Text = "This is a detail page. Press back to return.",
                                HorizontalAlignment = global::Avalonia.Layout.HorizontalAlignment.Center
                            }
                        }
                    }
                };
                await navPage.PushAsync(detailPage);
            };
        }
    }

    private void TabPlacementTop_Checked(object? sender, RoutedEventArgs e) {
        if (sender is RadioButton { IsChecked: true } && DemoTabbedPage != null)
            DemoTabbedPage.TabPlacement = TabPlacement.Top;
    }

    private void TabPlacementBottom_Checked(object? sender, RoutedEventArgs e) {
        if (sender is RadioButton { IsChecked: true } && DemoTabbedPage != null)
            DemoTabbedPage.TabPlacement = TabPlacement.Bottom;
    }

    private void TabPlacementLeft_Checked(object? sender, RoutedEventArgs e) {
        if (sender is RadioButton { IsChecked: true } && DemoTabbedPage != null)
            DemoTabbedPage.TabPlacement = TabPlacement.Left;
    }

    private void TabPlacementRight_Checked(object? sender, RoutedEventArgs e) {
        if (sender is RadioButton { IsChecked: true } && DemoTabbedPage != null)
            DemoTabbedPage.TabPlacement = TabPlacement.Right;
    }

    private void DrawerOverlay_Checked(object? sender, RoutedEventArgs e) {
        if (sender is RadioButton { IsChecked: true })
            UpdateDrawerDemo(DrawerLayoutBehavior.Overlay);
    }

    private void DrawerSplit_Checked(object? sender, RoutedEventArgs e) {
        if (sender is RadioButton { IsChecked: true } radioButton)
            UpdateDrawerDemo(DrawerLayoutBehavior.Split, radioButton.Content?.ToString() == "Persistent Split");
    }

    private void DrawerRtl_Checked(object? sender, RoutedEventArgs e) {
        if (sender is CheckBox checkBox)
            DemoDrawer.FlowDirection = checkBox.IsChecked == true ? global::Avalonia.Media.FlowDirection.RightToLeft : global::Avalonia.Media.FlowDirection.LeftToRight;

        if (sender is CheckBox)
            UpdateDrawerDemo(DemoDrawer?.DrawerLayoutBehavior ?? DrawerLayoutBehavior.Overlay, DemoDrawer?.DrawerBehavior == DrawerBehavior.Locked);
    }

    private void UpdateDrawerDemo(DrawerLayoutBehavior layoutBehavior, bool isPersistent = false) {
        if (DemoDrawer == null)
            return;
        DemoDrawer.DrawerBehavior = isPersistent ? DrawerBehavior.Locked : DrawerBehavior.Auto;
        DemoDrawer.DrawerLayoutBehavior = layoutBehavior;
        DemoDrawer.IsOpen = layoutBehavior == DrawerLayoutBehavior.Split;

    }

}
