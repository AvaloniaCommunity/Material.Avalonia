using System;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Material.Colors;

namespace Material.Demo.Extensions;

public class PrimaryColorExt : MarkupExtension
{
    public PrimaryColorExt()
    {
    }

    public PrimaryColorExt(PrimaryColor color)
    {
        Color = color;
    }

    [ConstructorArgument("color")] public PrimaryColor Color { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new SolidColorBrush(SwatchHelper.Lookup[(MaterialColor)Color]);
    }
}