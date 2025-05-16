using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia;
using Avalonia.Dialogs;
using ShowMeTheXaml;

namespace Material.Avalonia.Demo.Desktop {
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
        // CA1416: only Desktop platform supported (Windows, Linux, macOS)
        [SuppressMessage("Interoperability", "CA1416")]
        public static AppBuilder BuildAvaloniaApp() {
            return AppBuilder.Configure<App>()
                .LogToTrace()
                .UsePlatformDetect()
                .With(new X11PlatformOptions {
                    EnableMultiTouch = true,
                    UseDBusMenu = true,
                    EnableIme = true
                })
                .UseManagedSystemDialogs()
                .UseXamlDisplay();
        }
    }
}
