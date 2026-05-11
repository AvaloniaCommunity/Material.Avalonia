using Avalonia.Controls;
using Avalonia.Interactivity;

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
}
