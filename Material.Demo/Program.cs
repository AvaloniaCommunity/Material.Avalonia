using System;
using Avalonia;
using ShowMeTheXaml;

namespace Material.Demo {
    internal class Program {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.

        // STA thread is required for IME system.
        [STAThread]
        public static void Main(string[] args) {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args); //Start(AppMain, args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp() {
            return AppBuilder.Configure<App>()
                .LogToTrace()
                .UsePlatformDetect()
                .With(new X11PlatformOptions {
                    EnableMultiTouch = true,
                    UseDBusMenu = true,
                    EnableIme = true
                })
                .UseXamlDisplay();
        }
    }
}
