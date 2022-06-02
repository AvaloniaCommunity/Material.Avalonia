using System;
using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using ShowMeTheXaml;

namespace Material.Demo {
    internal class Program {

        public static MainWindow MainWindow { get; private set; }

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        
        // STA thread is required for IME system.
        [STAThread]
        public static void Main(string[] args)
        {
            BuildAvaloniaApp().Start(AppMain, args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp() {
            return AppBuilder.Configure<App>()
                             .UsePlatformDetect()
                             .With(new X11PlatformOptions
                             {
                                 EnableMultiTouch = true,
                                 UseDBusMenu = true,
                                 EnableIme = true
                             })
                             .With(new Win32PlatformOptions
                             {
                                 EnableMultitouch = true
                             })
                             .UseXamlDisplay()
                             .LogToTrace();
        }

        // Your application's entry point. Here you can initialize your MVVM framework, DI
        // container, etc.
        private static void AppMain(Application app, string[] args) {
            MainWindow = new MainWindow();
            app.Run(MainWindow);
        }
    }
}