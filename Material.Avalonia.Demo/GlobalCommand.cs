using System.Diagnostics;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Material.Styles.Themes;
using Material.Styles.Themes.Base;

namespace Material.Avalonia.Demo;

public static class GlobalCommand
{
    private static readonly MaterialThemeBase MaterialThemeStyles =
        Application.Current!.LocateMaterialTheme<MaterialThemeBase>();

    public static void UseMaterialUIDarkTheme()
    {
        MaterialThemeStyles.BaseTheme = BaseThemeMode.Dark;
    }

    public static void UseMaterialUILightTheme()
    {
        MaterialThemeStyles.BaseTheme = BaseThemeMode.Light;
    }

    public static void ToggleWindowExtendClientAreaToDecorationsHint()
    {
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && desktop.MainWindow != null) 
        {
            if (desktop.MainWindow.ExtendClientAreaToDecorationsHint) 
            {
                desktop.MainWindow.ExtendClientAreaToDecorationsHint = false;
                desktop.MainWindow.Title = "Material.Avalonia Demo";
            }
            else 
            {
                desktop.MainWindow.ExtendClientAreaToDecorationsHint = true;
                desktop.MainWindow.Title = " ";
            }
        }
    }

    public static void OpenProjectRepoLink() =>
        OpenBrowserForVisitSite("https://github.com/AvaloniaUtils/material.avalonia");

    public static void OpenBrowserForVisitSite(string link)
    {
        var param = new ProcessStartInfo
        {
            FileName = link,
            UseShellExecute = true,
            Verb = "open"
        };
        Process.Start(param);
    }
}