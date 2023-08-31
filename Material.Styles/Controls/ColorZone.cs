using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Media;
using Avalonia.Threading;
using Material.Styles.Themes;

namespace Material.Styles.Controls {
    public enum ColorZoneMode {
        Standard,
        Inverted,
        PrimaryLight,
        PrimaryMid,
        PrimaryDark,
        Accent,
        Light,
        Dark,
        Custom
    }

    public class ColorZone : ContentControl {
        public static readonly StyledProperty<ColorZoneMode> ModeProperty =
            AvaloniaProperty.Register<ColorZone, ColorZoneMode>(nameof(Mode));

        private IDisposable? _subscription;

        static ColorZone() {
            ModeProperty.Changed.Subscribe(OnNext);
        }

        public ColorZoneMode Mode {
            get => GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }

        private static void OnNext(AvaloniaPropertyChangedEventArgs<ColorZoneMode> arg) {
            if (arg.Sender is not ColorZone control)
                return;

            var resources = Application.Current!.LocateMaterialTheme<MaterialTheme>();
            control.OnThemeChanged(resources);
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e) {
            OnModeChanged(null);

            DisposeSubscription();

            var resources = Application.Current!.LocateMaterialTheme<MaterialTheme>();
            _subscription = resources.ThemeChangedEndObservable.Subscribe(OnThemeChanged);

            base.OnAttachedToVisualTree(e);
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e) {
            DisposeSubscription();

            base.OnDetachedFromVisualTree(e);
        }

        private void DisposeSubscription() {
            _subscription?.Dispose();
            _subscription = null;
        }

        private void OnThemeChanged(MaterialThemeBase theme) {
            Dispatcher.UIThread.InvokeAsync(delegate { OnModeChanged(theme); });
        }

        private void OnModeChanged(MaterialThemeBase? theme) {
            var colorZone = this;
            theme ??= Application.Current!.LocateMaterialTheme<MaterialTheme>();

            var resources = theme;

            var foregroundProperty = TemplatedControl.ForegroundProperty;

            switch (colorZone.Mode) {
                case ColorZoneMode.Standard: {
                    SetValueInternal(BackgroundProperty, GetBrushResource(resources, "MaterialPaperBrush"));
                    SetValueInternal(foregroundProperty, GetBrushResource(resources, "MaterialBodyBrush"));
                }
                    break;

                case ColorZoneMode.Inverted: {
                    SetValueInternal(BackgroundProperty, GetBrushResource(resources, "MaterialBodyBrush"));
                    SetValueInternal(foregroundProperty, GetBrushResource(resources, "MaterialPaperBrush"));
                }
                    break;

                case ColorZoneMode.Light: {
                    SetValueInternal(BackgroundProperty, GetBrushResource(resources, "MaterialLightBackgroundBrush"));
                    SetValueInternal(foregroundProperty, GetBrushResource(resources, "MaterialLightForegroundBrush"));
                }
                    break;

                case ColorZoneMode.Dark: {
                    SetValueInternal(BackgroundProperty, GetBrushResource(resources, "MaterialDarkBackgroundBrush"));
                    SetValueInternal(foregroundProperty, GetBrushResource(resources, "MaterialDarkForegroundBrush"));
                }
                    break;

                case ColorZoneMode.PrimaryLight: {
                    SetValueInternal(BackgroundProperty, GetBrushResource(resources, "MaterialPrimaryLightBrush"));
                    SetValueInternal(foregroundProperty, GetBrushResource(resources, "MaterialPrimaryLightForegroundBrush"));
                }
                    break;

                case ColorZoneMode.PrimaryMid: {
                    SetValueInternal(BackgroundProperty, GetBrushResource(resources, "MaterialPrimaryMidBrush"));
                    SetValueInternal(foregroundProperty, GetBrushResource(resources, "MaterialPrimaryMidForegroundBrush"));
                }
                    break;

                case ColorZoneMode.PrimaryDark: {
                    SetValueInternal(BackgroundProperty, GetBrushResource(resources, "MaterialPrimaryDarkBrush"));
                    SetValueInternal(foregroundProperty, GetBrushResource(resources, "MaterialPrimaryForegroundBrush"));
                }
                    break;

                case ColorZoneMode.Accent: {
                    SetValueInternal(BackgroundProperty, GetBrushResource(resources, "MaterialSecondaryMidBrush"));
                    SetValueInternal(foregroundProperty, GetBrushResource(resources, "MaterialSecondaryMidForegroundBrush"));
                }
                    break;

                case ColorZoneMode.Custom:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetValueInternal(AvaloniaProperty property, object? value) {
            SetValue(property, value, BindingPriority.Style);
        }

        private static IBrush? GetBrushResource(IResourceNode theme, string name) {
            if (!theme.TryGetResource(name, null, out var resource))
                return null;

            if (resource is IBrush brush)
                return brush;

            return null;
        }
    }
}
