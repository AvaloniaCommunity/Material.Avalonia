using System.Diagnostics;
using Avalonia;
using Material.Styles.Themes;
using Material.Styles.Themes.Base;

namespace Material.Demo
{
    public static class GlobalCommand
    {
        private static readonly MaterialTheme MaterialThemeStyles =
            Application.Current!.LocateMaterialTheme<MaterialTheme>();

        public static void UseMaterialUIDarkTheme()
        {
            MaterialThemeStyles.BaseTheme = BaseThemeMode.Dark;
        }

        public static void UseMaterialUILightTheme()
        {
            MaterialThemeStyles.BaseTheme = BaseThemeMode.Light;
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
}