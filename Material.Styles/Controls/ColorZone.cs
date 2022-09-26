using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Media;
using Avalonia.Threading;
using Material.Styles.Themes;

namespace Material.Styles.Controls
{
    public enum ColorZoneMode
    {
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

    public class ColorZone : ContentControl
    {
        public static readonly StyledProperty<ColorZoneMode> ModeProperty =
            AvaloniaProperty.Register<ColorZone, ColorZoneMode>(nameof(Mode));

        public ColorZoneMode Mode
        {
            get => GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }

        private IDisposable? _subscription;

        static ColorZone()
        {
            ModeProperty.Changed.Subscribe(OnNext);
        }

        private static void OnNext(AvaloniaPropertyChangedEventArgs<ColorZoneMode> arg)
        {
            if (arg.Sender is not ColorZone control)
                return;

            var resources = Application.Current!.LocateMaterialTheme<MaterialTheme>();
            control.OnThemeChanged(resources);
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            OnModeChanged(null);

            DisposeSubscription();

            var resources = Application.Current!.LocateMaterialTheme<MaterialTheme>();
            _subscription = resources.ThemeChangedObservable.Subscribe(OnThemeChanged);

            base.OnAttachedToVisualTree(e);
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            DisposeSubscription();

            base.OnDetachedFromVisualTree(e);
        }

        private void DisposeSubscription()
        {
            _subscription?.Dispose();
            _subscription = null;
        }

        private void OnThemeChanged(MaterialThemeBase theme)
        {
            Dispatcher.UIThread.InvokeAsync(delegate { OnModeChanged(theme); });
        }

        private void OnModeChanged(MaterialThemeBase? theme)
        {
            var colorZone = this;
            theme ??= Application.Current!.LocateMaterialTheme<MaterialTheme>();

            var resources = theme;

            var foregroundProperty = TemplatedControl.ForegroundProperty;

            switch (colorZone.Mode)
            {
                case ColorZoneMode.Standard:
                {
                    SetValueInternal(BackgroundProperty, GetBrushResource(resources, "MaterialDesignPaper"));
                    SetValueInternal(foregroundProperty, GetBrushResource(resources, "MaterialDesignBody"));
                }
                    break;

                case ColorZoneMode.Inverted:
                {
                    SetValueInternal(BackgroundProperty, GetBrushResource(resources, "MaterialDesignBody"));
                    SetValueInternal(foregroundProperty, GetBrushResource(resources, "MaterialDesignPaper"));
                }
                    break;

                case ColorZoneMode.Light:
                {
                    SetValueInternal(BackgroundProperty, GetBrushResource(resources, "MaterialDesignLightBackground"));
                    SetValueInternal(foregroundProperty, GetBrushResource(resources, "MaterialDesignLightForeground"));
                }
                    break;

                case ColorZoneMode.Dark:
                {
                    SetValueInternal(BackgroundProperty, GetBrushResource(resources, "MaterialDesignDarkBackground"));
                    SetValueInternal(foregroundProperty, GetBrushResource(resources, "MaterialDesignDarkForeground"));
                }
                    break;

                case ColorZoneMode.PrimaryLight:
                {
                    SetValueInternal(BackgroundProperty, GetBrushResource(resources, "PrimaryHueLightBrush"));
                    SetValueInternal(foregroundProperty, GetBrushResource(resources, "PrimaryHueLightForegroundBrush"));
                }
                    break;

                case ColorZoneMode.PrimaryMid:
                {
                    SetValueInternal(BackgroundProperty, GetBrushResource(resources, "PrimaryHueMidBrush"));
                    SetValueInternal(foregroundProperty, GetBrushResource(resources, "PrimaryHueMidForegroundBrush"));
                }
                    break;

                case ColorZoneMode.PrimaryDark:
                {
                    SetValueInternal(BackgroundProperty, GetBrushResource(resources, "PrimaryHueDarkBrush"));
                    SetValueInternal(foregroundProperty, GetBrushResource(resources, "PrimaryHueDarkForegroundBrush"));
                }
                    break;

                case ColorZoneMode.Accent:
                {
                    SetValueInternal(BackgroundProperty, GetBrushResource(resources, "SecondaryHueMidBrush"));
                    SetValueInternal(foregroundProperty, GetBrushResource(resources, "SecondaryHueMidForegroundBrush"));
                }
                    break;

                case ColorZoneMode.Custom:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetValueInternal(AvaloniaProperty property, object? value)
        {
            SetValue(property, value, BindingPriority.Style);
        }

        private static IBrush? GetBrushResource(IResourceNode theme, string name)
        {
            if (!theme.TryGetResource(name, out var resource))
                return null;

            if (resource is IBrush brush)
                return brush;

            return null;
        }
    }
}