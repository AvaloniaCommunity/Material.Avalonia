using Material.Colors;
using Material.Styles.Themes;
using Material.Styles.Themes.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Material.Demo
{
    public static class GlobalCommand
    {
        private static PaletteHelper m_paletteHelper;
        private static PaletteHelper PaletteHelper 
        { 
            get 
            { 
                if (m_paletteHelper is null) 
                    m_paletteHelper = new PaletteHelper();
                return m_paletteHelper;
            } 
        }

        public static void UseMaterialUIDarkTheme()
        {
            var theme = PaletteHelper.GetTheme();
            theme.SetPrimaryColor(SwatchHelper.Lookup[MaterialColor.Blue200]);
            theme.SetSecondaryColor(SwatchHelper.Lookup[MaterialColor.Pink200]);
            theme.SetBaseTheme(BaseThemeMode.Dark.GetBaseTheme());
            PaletteHelper.SetTheme(theme);
        }
        public static void UseMaterialUILightTheme()
        {
            var theme = PaletteHelper.GetTheme();
            theme.SetPrimaryColor(SwatchHelper.Lookup[MaterialColor.Blue]);
            theme.SetSecondaryColor(SwatchHelper.Lookup[MaterialColor.Pink400]);
            theme.SetBaseTheme(BaseThemeMode.Light.GetBaseTheme());
            PaletteHelper.SetTheme(theme);
        }

        public static void OpenProjectRepoLink() => OpenBrowserForVisitSite("https://github.com/AvaloniaUtils/material.avalonia");

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
