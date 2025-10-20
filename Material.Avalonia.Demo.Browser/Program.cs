using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Browser;
using Avalonia.Media;
using Avalonia.Media.Fonts;
using ShowMeTheXaml;

[assembly: SupportedOSPlatform("browser")]

namespace Material.Avalonia.Demo.Browser;

internal static class Program {
    private static Task Main() => BuildAvaloniaApp()
        .StartBrowserAppAsync("out");

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .ConfigureFonts(manager => {
                manager.AddFontCollection(
                    new EmbeddedFontCollection(new Uri("fonts:OpenMoji", UriKind.Absolute),
                        new Uri("avares://Material.Avalonia.Demo.Browser/Assets", UriKind.Absolute)));
            })
            .With(new FontManagerOptions {
                DefaultFamilyName = "avares://Material.Avalonia.Demo.Browser/Assets#OpenMoji",
            })
            .UseXamlDisplay();
}