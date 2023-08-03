using System;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Material.Colors;

namespace Material.Demo.Extensions;

public class SecondaryColorExt : MarkupExtension
{
    public SecondaryColorExt()
    {
    }

    public SecondaryColorExt(SecondaryColor color)
    {
        Color = color;
    }

    [ConstructorArgument("color")] public SecondaryColor Color { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new SolidColorBrush(SwatchHelper.Lookup[(MaterialColor)Color]);
    }
}