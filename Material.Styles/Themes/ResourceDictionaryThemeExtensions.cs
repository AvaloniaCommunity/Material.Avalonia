using System;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;
using Material.Colors;
using Material.Colors.ColorManipulation;

namespace Material.Styles.Themes {
    public static class ResourceDictionaryExtensions {
        private static Guid CurrentThemeKey { get; } = Guid.NewGuid();
        private static Guid ThemeManagerKey { get; } = Guid.NewGuid();

        [Obsolete(
            $"Obsolete styling system. Use {nameof(MaterialTheme)}. Details in our wiki: https://github.com/AvaloniaCommunity/Material.Avalonia/wiki/Advanced-Theming")]
        public static void SetTheme(this IResourceDictionary resourceDictionary, ITheme theme) {
            SetThemeInternal(resourceDictionary, theme);

            if (!(resourceDictionary.GetThemeManager() is ThemeManager themeManager))
                resourceDictionary[ThemeManagerKey] = themeManager = new ThemeManager(resourceDictionary);

            var oldTheme = resourceDictionary.TryGetResource(CurrentThemeKey, null, out var oldThemeTemp)
                ? oldThemeTemp as ITheme
                : null;
            resourceDictionary[CurrentThemeKey] = theme;

            themeManager.OnThemeChange(oldTheme, theme);
        }

        internal static void SetThemeInternal(this IResourceDictionary resourceDictionary, ITheme theme) {
            if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));

            SetSolidColorBrush(resourceDictionary, "MaterialPrimaryLightBrush", theme.PrimaryLight.Color);
            SetSolidColorBrush(resourceDictionary, "MaterialPrimaryLightForegroundBrush",
                theme.PrimaryLight.ForegroundColor);
            SetSolidColorBrush(resourceDictionary, "MaterialPrimaryMidBrush", theme.PrimaryMid.Color);
            SetSolidColorBrush(resourceDictionary, "MaterialPrimaryMidForegroundBrush", theme.PrimaryMid.ForegroundColor);
            SetSolidColorBrush(resourceDictionary, "MaterialPrimaryDarkBrush", theme.PrimaryDark.Color);
            SetSolidColorBrush(resourceDictionary, "MaterialPrimaryForegroundBrush", theme.PrimaryDark.ForegroundColor);

            SetSolidColorBrush(resourceDictionary, "MaterialSecondaryLightBrush", theme.SecondaryLight.Color);
            SetSolidColorBrush(resourceDictionary, "MaterialSecondaryLightForegroundBrush",
                theme.SecondaryLight.ForegroundColor);
            SetSolidColorBrush(resourceDictionary, "MaterialSecondaryMidBrush", theme.SecondaryMid.Color);
            SetSolidColorBrush(resourceDictionary, "MaterialSecondaryMidForegroundBrush",
                theme.SecondaryMid.ForegroundColor);
            SetSolidColorBrush(resourceDictionary, "MaterialSecondaryDarkBrush", theme.SecondaryDark.Color);
            SetSolidColorBrush(resourceDictionary, "MaterialSecondaryDarkForegroundBrush",
                theme.SecondaryDark.ForegroundColor);

            SetSolidColorBrush(resourceDictionary, "MaterialValidationErrorBrush", theme.ValidationError);
            // SetSolidColorBrush(resourceDictionary, "MaterialValidationErrorForegroundBrush", theme.ValidationError.ForegroundColor);

            SetSolidColorBrush(resourceDictionary, "MaterialBackgroundBrush", theme.Background);
            SetSolidColorBrush(resourceDictionary, "MaterialPaperBrush", theme.Paper);
            SetSolidColorBrush(resourceDictionary, "MaterialCardBackgroundBrush", theme.CardBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialToolBarBackgroundBrush", theme.ToolBarBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialBodyBrush", theme.Body);
            SetSolidColorBrush(resourceDictionary, "MaterialBodyLightBrush", theme.BodyLight);
            SetSolidColorBrush(resourceDictionary, "MaterialColumnHeaderBrush", theme.ColumnHeader);
            SetSolidColorBrush(resourceDictionary, "MaterialCheckBoxOffBrush", theme.CheckBoxOff);
            SetSolidColorBrush(resourceDictionary, "MaterialCheckBoxDisabledBrush", theme.CheckBoxDisabled);
            SetSolidColorBrush(resourceDictionary, "MaterialTextBoxBorderBrush", theme.TextBoxBorder);
            SetSolidColorBrush(resourceDictionary, "MaterialDividerBrush", theme.Divider);
            SetSolidColorBrush(resourceDictionary, "MaterialSelectionBrush", theme.Selection);
            SetSolidColorBrush(resourceDictionary, "MaterialToolForegroundBrush", theme.ToolForeground);
            SetSolidColorBrush(resourceDictionary, "MaterialToolBackgroundBrush", theme.ToolBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialFlatButtonClickBrush", theme.FlatButtonClick);
            SetSolidColorBrush(resourceDictionary, "MaterialFlatButtonRippleBrush", theme.FlatButtonRipple);
            SetSolidColorBrush(resourceDictionary, "MaterialToolTipBackgroundBrush", theme.ToolTipBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialChipBackgroundBrush", theme.ChipBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialSnackbarBackgroundBrush", theme.SnackbarBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialSnackbarMouseOverBrush", theme.SnackbarMouseOver);
            SetSolidColorBrush(resourceDictionary, "MaterialSnackbarRippleBrush", theme.SnackbarRipple);
            SetSolidColorBrush(resourceDictionary, "MaterialTextFieldBoxBackgroundBrush",
                theme.TextFieldBoxBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialTextFieldBoxHoverBackgroundBrush",
                theme.TextFieldBoxHoverBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialTextFieldBoxDisabledBackgroundBrush",
                theme.TextFieldBoxDisabledBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialTextAreaBorderBrush", theme.TextAreaBorder);
            SetSolidColorBrush(resourceDictionary, "MaterialTextAreaInactiveBorderBrush",
                theme.TextAreaInactiveBorder);
            SetSolidColorBrush(resourceDictionary, "MaterialDataGridRowHoverBackgroundBrush",
                theme.DataGridRowHoverBackground);
        }

        [Obsolete(
            $"Obsolete styling system. Use {nameof(MaterialTheme)}. Details in our wiki: https://github.com/AvaloniaCommunity/Material.Avalonia/wiki/Advanced-Theming")]
        public static ITheme GetTheme(this IResourceDictionary resourceDictionary) {
            if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));
            if (resourceDictionary.TryGetResource(CurrentThemeKey, null, out var theme) && theme is ITheme)
                return (ITheme)theme;

            var secondaryMid = GetColor("MaterialSecondaryMidBrush");
            var secondaryMidForeground = GetColor("MaterialSecondaryMidForegroundBrush");

            if (!TryGetColor("MaterialSecondaryLightBrush", out var secondaryLight))
                secondaryLight = secondaryMid.Lighten();

            if (!TryGetColor("MaterialSecondaryLightForegroundBrush", out var secondaryLightForeground))
                secondaryLightForeground = secondaryLight.PickContrastColor();

            if (!TryGetColor("MaterialSecondaryDarkBrush", out var secondaryDark))
                secondaryDark = secondaryMid.Darken();

            if (!TryGetColor("MaterialSecondaryDarkForegroundBrush", out var secondaryDarkForeground))
                secondaryDarkForeground = secondaryDark.PickContrastColor();

            //Attempt to simply look up the appropriate resources
            return new Theme {
                PrimaryLight = new ColorPair(GetColor("MaterialPrimaryLightBrush"),
                    GetColor("MaterialPrimaryLightForegroundBrush")),
                PrimaryMid = new ColorPair(GetColor("MaterialPrimaryMidBrush"), GetColor("MaterialPrimaryMidForegroundBrush")),
                PrimaryDark = new ColorPair(GetColor("MaterialPrimaryDarkBrush"), GetColor("MaterialPrimaryForegroundBrush")),

                SecondaryLight = new ColorPair(secondaryLight, secondaryLightForeground),
                SecondaryMid = new ColorPair(secondaryMid, secondaryMidForeground),
                SecondaryDark = new ColorPair(secondaryDark, secondaryDarkForeground),

                Background = GetColor("MaterialBackgroundBrush"),
                Body = GetColor("MaterialBodyBrush"),
                BodyLight = GetColor("MaterialBodyLightBrush"),
                CardBackground = GetColor("MaterialCardBackgroundBrush"),
                CheckBoxDisabled = GetColor("MaterialCheckBoxDisabledBrush"),
                CheckBoxOff = GetColor("MaterialCheckBoxOffBrush"),
                ChipBackground = GetColor("MaterialChipBackgroundBrush"),
                ColumnHeader = GetColor("MaterialColumnHeaderBrush"),
                DataGridRowHoverBackground = GetColor("MaterialDataGridRowHoverBackgroundBrush"),
                Divider = GetColor("MaterialDividerBrush"),
                FlatButtonClick = GetColor("MaterialFlatButtonClickBrush"),
                FlatButtonRipple = GetColor("MaterialFlatButtonRippleBrush"),
                Selection = GetColor("MaterialSelectionBrush"),
                SnackbarBackground = GetColor("MaterialSnackbarBackgroundBrush"),
                SnackbarMouseOver = GetColor("MaterialSnackbarMouseOverBrush"),
                SnackbarRipple = GetColor("MaterialSnackbarRippleBrush"),
                TextAreaBorder = GetColor("MaterialTextAreaBorderBrush"),
                TextAreaInactiveBorder = GetColor("MaterialTextAreaInactiveBorderBrush"),
                TextBoxBorder = GetColor("MaterialTextBoxBorderBrush"),
                TextFieldBoxBackground = GetColor("MaterialTextFieldBoxBackgroundBrush"),
                TextFieldBoxDisabledBackground = GetColor("MaterialTextFieldBoxDisabledBackgroundBrush"),
                TextFieldBoxHoverBackground = GetColor("MaterialTextFieldBoxHoverBackgroundBrush"),
                ToolBackground = GetColor("MaterialToolBackgroundBrush"),
                ToolBarBackground = GetColor("MaterialToolBarBackgroundBrush"),
                ToolForeground = GetColor("MaterialToolForegroundBrush"),
                ToolTipBackground = GetColor("MaterialToolTipBackgroundBrush"),
                Paper = GetColor("MaterialPaperBrush"),
                ValidationError = GetColor("MaterialValidationErrorBrush")
            };

            Color GetColor(string key) {
                if (TryGetColor(key, out var color))
                    return color;

                throw new InvalidOperationException($"Could not locate required resource with key '{key}'");
            }

            bool TryGetColor(string key, out Color color) {
                if (resourceDictionary[key] is SolidColorBrush brush) {
                    color = brush.Color;
                    return true;
                }

                color = default;
                return false;
            }
        }

        [Obsolete(
            $"Obsolete styling system. Use {nameof(MaterialTheme)}. Details in our wiki: https://github.com/AvaloniaCommunity/Material.Avalonia/wiki/Advanced-Theming")]
        public static IThemeManager? GetThemeManager(this IResourceDictionary resourceDictionary) {
            if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));

            return resourceDictionary.TryGetResource(ThemeManagerKey, null, out var manager)
                ? manager as IThemeManager
                : null;
        }

        internal static void SetSolidColorBrush(this IResourceDictionary sourceDictionary, string name, Color value) {
            if (sourceDictionary == null) throw new ArgumentNullException(nameof(sourceDictionary));
            if (name == null) throw new ArgumentNullException(nameof(name));

            if (sourceDictionary.TryGetValue(name + "Color", out var currentValue) &&
                currentValue as Color? == value) return;
            sourceDictionary[name + "Color"] = value;

            if (sourceDictionary.ContainsKey(name) && sourceDictionary[name] is SolidColorBrush brush) {
                Dispatcher.UIThread.InvokeAsync(delegate {
                    if (brush.Color == value)
                        return;

                    if (brush.Transitions == null || brush.Transitions.Count == 0) {
                        brush.Transitions = new Transitions {
                            new ColorTransition {
                                Duration = TimeSpan.FromSeconds(0.35), Easing = new SineEaseOut(),
                                Property = SolidColorBrush.ColorProperty
                            }
                        };
                    }

                    brush.Color = value;
                });

                return;
            }

            Dispatcher.UIThread.InvokeAsync(delegate {
                var newBrush = new SolidColorBrush(value);
                sourceDictionary[name] = newBrush;
            });
        }

        [Obsolete(
            $"Obsolete styling system. Use {nameof(MaterialTheme)}. Details in our wiki: https://github.com/AvaloniaCommunity/Material.Avalonia/wiki/Advanced-Theming")]
        private class ThemeManager : IThemeManager {
            private readonly IResourceDictionary _resourceDictionary;

            public ThemeManager(IResourceDictionary resourceDictionary) {
                _resourceDictionary = resourceDictionary ?? throw new ArgumentNullException(nameof(resourceDictionary));
            }

            public event EventHandler<ThemeChangedEventArgs>? ThemeChanged;

            public void OnThemeChange(ITheme? oldTheme, ITheme newTheme) {
                ThemeChanged?.Invoke(this, new ThemeChangedEventArgs(_resourceDictionary, oldTheme, newTheme));
            }
        }
    }
}
