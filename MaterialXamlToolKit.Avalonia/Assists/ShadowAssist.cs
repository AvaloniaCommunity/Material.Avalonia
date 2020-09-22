using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace MaterialXamlToolKit.Avalonia.Assists {
    public static class ShadowProvider {
        public static Color MaterialShadowColor { get; set; } = Color.FromArgb(107, 0, 0, 0);

        public static BoxShadows ToBoxShadows(this ShadowDepth shadowDepth) {
            return shadowDepth switch {
                ShadowDepth.Depth0 => new BoxShadows(new BoxShadow()),
                ShadowDepth.Depth1 => new BoxShadows(new BoxShadow {Blur = 5, OffsetX = 1, OffsetY = 1, Color = MaterialShadowColor}),
                ShadowDepth.Depth2 => new BoxShadows(new BoxShadow {Blur = 8, OffsetX = 1.5, OffsetY = 1.5, Color = MaterialShadowColor}),
                ShadowDepth.Depth3 => new BoxShadows(new BoxShadow {Blur = 14, OffsetX = 4.5, OffsetY = 4.5, Color = MaterialShadowColor}),
                ShadowDepth.Depth4 => new BoxShadows(new BoxShadow {Blur = 25, OffsetX = 8, OffsetY = 8, Color = MaterialShadowColor}),
                ShadowDepth.Depth5 => new BoxShadows(new BoxShadow {Blur = 35, OffsetX = 13, OffsetY = 13, Color = MaterialShadowColor}),
                _                  => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public enum ShadowDepth {
        Depth0,
        Depth1,
        Depth2,
        Depth3,
        Depth4,
        Depth5
    }

    public static class ShadowAssist {
        static ShadowAssist() {
            ShadowDepthProperty.Changed.Subscribe(args => {
                if (args.Sender is Border border) {
                    border.BoxShadow = (args.NewValue as ShadowDepth? ?? ShadowDepth.Depth0).ToBoxShadows();
                }
            });
        }
        
        public static readonly AvaloniaProperty<ShadowDepth> ShadowDepthProperty = AvaloniaProperty.RegisterAttached<AvaloniaObject, ShadowDepth>(
            "ShadowDepth", typeof(ShadowAssist), ShadowDepth.Depth0, true);

        public static void SetShadowDepth(AvaloniaObject element, ShadowDepth value) {
            element.SetValue(ShadowDepthProperty, value);
        }

        public static ShadowDepth GetShadowDepth(AvaloniaObject element) {
            return (ShadowDepth) element.GetValue(ShadowDepthProperty);
        }
    }
}