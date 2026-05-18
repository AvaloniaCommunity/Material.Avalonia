using System;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Metadata;
using Material.Icons;

namespace Material.Avalonia.Demo.Extensions
{
    public class MaterialIconGeometryExt : MarkupExtension
    {
        public MaterialIconGeometryExt() { }

        public MaterialIconGeometryExt(MaterialIconKind kind)
        {
            Kind = kind;
        }

        [ConstructorArgument("kind")]
        public MaterialIconKind Kind { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var data = MaterialIconDataProvider.GetData(Kind);
            return StreamGeometry.Parse(data);
        }
    }
}
