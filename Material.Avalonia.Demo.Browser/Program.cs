using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Browser;
using ShowMeTheXaml;

[assembly: SupportedOSPlatform("browser")]

namespace Material.Avalonia.Demo.Browser;

internal static class Program
{
    private static Task Main() => BuildAvaloniaApp()
        .StartBrowserAppAsync("out");

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UseXamlDisplay();
}