using System;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Input.Platform;

namespace Material.Styles.Converters
{
    public class GetPlatformHotkeyConfigServiceConverter : IValueConverter
    {
        private PlatformHotkeyConfiguration _config;

        public GetPlatformHotkeyConfigServiceConverter()
        {
            _config = AvaloniaLocator.Current.GetService<PlatformHotkeyConfiguration>();
        }

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (parameter is not string kind)
                return null;

            var gestures = kind switch
            {
                "copy" => _config.Copy,
                "cut" => _config.Cut,
                "paste" => _config.Paste,
                "selectall" => _config.SelectAll,
                "undo" => _config.Undo,
                "redo" => _config.Redo,
                _ => null
            };

            return gestures?.FirstOrDefault();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}