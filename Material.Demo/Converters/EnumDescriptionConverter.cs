using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Avalonia.Data.Converters;

namespace Material.Demo.Converters {
    public class EnumDescriptionConverter : IValueConverter {
        object IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
            var myEnum = (Enum)value;
            var description = GetEnumDescription(myEnum);
            return description;
        }

        object IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
            return string.Empty;
        }
        private string GetEnumDescription(Enum enumObj) {
            var fieldInfo = enumObj.GetType().GetField(enumObj.ToString());
            var descriptionAttr = fieldInfo
                .GetCustomAttributes(false)
                .OfType<DescriptionAttribute>()
                .Cast<DescriptionAttribute>()
                .SingleOrDefault();
            if (descriptionAttr == null) {
                return enumObj.ToString();
            }
            else {
                return descriptionAttr.Description;
            }
        }
    }
}
