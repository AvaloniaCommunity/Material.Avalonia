using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Material.Avalonia.Demo.Assists;

namespace Material.Avalonia.Demo;

public class App : Application {
    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted() {
        // initiate AutomationAssist to attach events automatically
        AutomationAssist.Placeholder();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = new MainWindow();
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform) {
            singleViewPlatform.MainView = new MainView();
        }

        base.OnFrameworkInitializationCompleted();
    }
}