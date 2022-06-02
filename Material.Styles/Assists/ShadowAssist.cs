using System;
using System.Globalization;
using System.Threading;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;

namespace Material.Styles.Assists {
    public static class ShadowProvider {
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public static Color MaterialShadowColor { get; set; } = Color.FromArgb(76, 0, 0, 0);

        public static BoxShadows ToBoxShadows(this ShadowDepth shadowDepth, Color? overrideColor = null) {
            return shadowDepth switch {
                ShadowDepth.Depth0 => new BoxShadows(new BoxShadow()),
                ShadowDepth.Depth1 => new BoxShadows(new BoxShadow {Blur = 5, OffsetX = 1, OffsetY = 1, Color = overrideColor ?? MaterialShadowColor}),
                ShadowDepth.Depth2 => new BoxShadows(new BoxShadow {Blur = 8, OffsetX = 1.5, OffsetY = 1.5, Color = overrideColor ?? MaterialShadowColor}),
                ShadowDepth.Depth3 => new BoxShadows(new BoxShadow {Blur = 14, OffsetX = 4.5, OffsetY = 4.5, Color = overrideColor ?? MaterialShadowColor}),
                ShadowDepth.Depth4 => new BoxShadows(new BoxShadow {Blur = 25, OffsetX = 8, OffsetY = 8, Color = overrideColor ?? MaterialShadowColor}),
                ShadowDepth.Depth5 => new BoxShadows(new BoxShadow {Blur = 35, OffsetX = 13, OffsetY = 13, Color = overrideColor ?? MaterialShadowColor}),
                ShadowDepth.CenterDepth1 => new BoxShadows(new BoxShadow { Blur = 5, OffsetY = 1, Color = overrideColor ?? MaterialShadowColor }),
                ShadowDepth.CenterDepth2 => new BoxShadows(new BoxShadow { Blur = 8, OffsetY = 1.5, Color = overrideColor ?? MaterialShadowColor }),
                ShadowDepth.CenterDepth3 => new BoxShadows(new BoxShadow { Blur = 14, OffsetY = 4.5, Color = overrideColor ?? MaterialShadowColor }),
                ShadowDepth.CenterDepth4 => new BoxShadows(new BoxShadow { Blur = 25, OffsetY = 8, Color = overrideColor ?? MaterialShadowColor }),
                ShadowDepth.CenterDepth5 => new BoxShadows(new BoxShadow { Blur = 35, OffsetY = 13, Color = overrideColor ?? MaterialShadowColor }),
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
        Depth5, 
        CenterDepth1,
        CenterDepth2,
        CenterDepth3,
        CenterDepth4,
        CenterDepth5
    }

    public static class ShadowAssist {
        public static readonly AvaloniaProperty<ShadowDepth> ShadowDepthProperty =
            AvaloniaProperty.RegisterAttached<AvaloniaObject, ShadowDepth>(
            "ShadowDepth", typeof(ShadowAssist));

        public static readonly AvaloniaProperty<bool> DarkenProperty = 
            AvaloniaProperty.RegisterAttached<AvaloniaObject, bool>(
            "Darken", typeof(ShadowAssist));

        static ShadowAssist() {
            ShadowDepthProperty.Changed.Subscribe(ShadowDepthChangedCallback);
            DarkenProperty.Changed.Subscribe(DarkenPropertyChangedCallback);
        }

        private static void ShadowDepthChangedCallback(AvaloniaPropertyChangedEventArgs args) {
            if (args.Sender is Border border)
                border.BoxShadow = 
                    (args.NewValue as ShadowDepth? ?? ShadowDepth.Depth0)
                    .ToBoxShadows();
        }

        public static void SetShadowDepth(AvaloniaObject element, ShadowDepth value) 
            => element.SetValue<ShadowDepth>(ShadowDepthProperty, value);

        public static ShadowDepth GetShadowDepth(AvaloniaObject element) 
            => element.GetValue<ShadowDepth>(ShadowDepthProperty);

        private static void DarkenPropertyChangedCallback(AvaloniaPropertyChangedEventArgs obj)
        {
            if (obj.Sender is not Border border)
                return;

            var boxShadow = border.BoxShadow;

            var targetBoxShadows = (bool?) obj.NewValue == true
                ? GetShadowDepth(border).ToBoxShadows(Color.FromArgb(255, 0, 0, 0))
                : GetShadowDepth(border).ToBoxShadows();

            if (!border.Classes.Contains("no-transitions"))
            {
                var animation = new Animation {Duration = TimeSpan.FromMilliseconds(350), FillMode = FillMode.Both};
                animation.Children.Add(
                    new KeyFrame {
                        Cue = Cue.Parse("0%", CultureInfo.CurrentCulture),
                        Setters = {
                            new Setter
                            {
                                Property = Border.BoxShadowProperty, 
                                Value = boxShadow
                            }
                        }
                    });
                animation.Children.Add(
                    new KeyFrame {
                        Cue = Cue.Parse("100%", CultureInfo.CurrentCulture),
                        Setters = {
                            new Setter
                            {
                                Property = Border.BoxShadowProperty, 
                                Value = targetBoxShadows
                            }
                        }
                    });
                animation.RunAsync(border, null, CancellationToken.None);
            }
            else
            {
                border.SetValue(Border.BoxShadowProperty, targetBoxShadows);
            }
        }

        public static void SetDarken(AvaloniaObject element, bool value) 
            => element.SetValue<bool>(DarkenProperty, value);

        public static bool GetDarken(AvaloniaObject element)
            => element.GetValue<bool>(DarkenProperty);
    }
}