using System;
using System.Runtime.InteropServices;
using Avalonia.Logging;
using Material.Styles.Themes.Base;

namespace Material.Styles.Themes;

public static class SystemThemeProbe {
    /// <summary>
    /// Tries to resolve base theme mode for the current system
    /// </summary>
    /// <returns>Base theme (<c>Dark</c>/<c>Light</c>) or <c>null</c></returns>
    public static BaseThemeMode? GetSystemBaseThemeMode() {
        try {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return GetWindowsBaseThemeMode();
        }
        catch (Exception e) {
            Logger.TryGet(LogEventLevel.Error, "Material.Themes")
                ?.Log("SystemThemeProbe", "Failed to get system base theme: {Exception}", e);
        }

        return null;
    }


    [DllImport("advapi32.dll", EntryPoint = "RegOpenKeyEx")]
    private static extern int RegOpenKeyEx_DllImport(UIntPtr hKey, string lpSubKey, uint ulOptions, int samDesired, out UIntPtr phkResult);
    [DllImport("advapi32.dll", EntryPoint = "RegQueryValueEx")]
    private static extern int RegQueryValueEx_DllImport(UIntPtr hKey, string lpValueName, int lpReserved, out uint lpType, byte[] lpData, ref int lpcbData);
    private static readonly UIntPtr HKEY_CURRENT_USER = (UIntPtr)0x80000001;

    /// <summary>
    /// Retrieving windows base theme from registry
    /// </summary>
    /// <remarks>
    /// Relies on the <c>HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize</c> and <c>AppsUseLightTheme</c> key
    /// </remarks>
    /// <exception cref="Exception">
    /// Can probably throw exception if something gone wrong :shrug:
    /// </exception>
    /// <returns>Base theme (<c>Dark</c>/<c>Light</c>)</returns>
    public static BaseThemeMode GetWindowsBaseThemeMode() {
        var infoDataLength = 4;
        var infoBytes = new byte[infoDataLength];

        // I implemented it via P/Invoke cuz i dont want to see additional package reference to Microsoft.Win32.Registry in Material.Avalonia
        var o0 = RegOpenKeyEx_DllImport(HKEY_CURRENT_USER, "Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", 0, 0x1, out var hKeyVal);
        if (o0 != 0) throw new Exception("Something went wrong when opening \"Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize\" registry entry");
        var o1 = RegQueryValueEx_DllImport(hKeyVal, "AppsUseLightTheme", 0, out _, infoBytes, ref infoDataLength);
        if (o1 != 0) throw new Exception("Something went wrong when reading \"AppsUseLightTheme\" registry entry value");

        return BitConverter.ToBoolean(infoBytes, 0) ? BaseThemeMode.Dark : BaseThemeMode.Light;
    }
}