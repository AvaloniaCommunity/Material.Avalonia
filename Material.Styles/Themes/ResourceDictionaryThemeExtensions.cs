using System;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;
using JetBrains.Annotations;
using Material.Colors;
using Material.Colors.ColorManipulation;

namespace Material.Styles.Themes {
    public static class ResourceDictionaryExtensions {
        private static Guid CurrentThemeKey { get; } = Guid.NewGuid();
        private static Guid ThemeManagerKey { get; } = Guid.NewGuid();

        public static void SetTheme(this IResourceDictionary resourceDictionary, ITheme theme) {
            if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));

            SetSolidColorBrush(resourceDictionary, "PrimaryHueLightBrush", theme.PrimaryLight.Color);
            SetSolidColorBrush(resourceDictionary, "PrimaryHueLightForegroundBrush",
                theme.PrimaryLight.ForegroundColor ?? theme.PrimaryLight.Color.ContrastingForegroundColor());
            SetSolidColorBrush(resourceDictionary, "PrimaryHueMidBrush", theme.PrimaryMid.Color);
            SetSolidColorBrush(resourceDictionary, "PrimaryHueMidForegroundBrush",
                theme.PrimaryMid.ForegroundColor ?? theme.PrimaryMid.Color.ContrastingForegroundColor());
            SetSolidColorBrush(resourceDictionary, "PrimaryHueDarkBrush", theme.PrimaryDark.Color);
            SetSolidColorBrush(resourceDictionary, "PrimaryHueDarkForegroundBrush",
                theme.PrimaryDark.ForegroundColor ?? theme.PrimaryDark.Color.ContrastingForegroundColor());

            SetSolidColorBrush(resourceDictionary, "SecondaryHueLightBrush", theme.SecondaryLight.Color);
            SetSolidColorBrush(resourceDictionary, "SecondaryHueLightForegroundBrush",
                theme.SecondaryLight.ForegroundColor ?? theme.SecondaryLight.Color.ContrastingForegroundColor());
            SetSolidColorBrush(resourceDictionary, "SecondaryHueMidBrush", theme.SecondaryMid.Color);
            SetSolidColorBrush(resourceDictionary, "SecondaryHueMidForegroundBrush",
                theme.SecondaryMid.ForegroundColor ?? theme.SecondaryMid.Color.ContrastingForegroundColor());
            SetSolidColorBrush(resourceDictionary, "SecondaryHueDarkBrush", theme.SecondaryDark.Color);
            SetSolidColorBrush(resourceDictionary, "SecondaryHueDarkForegroundBrush",
                theme.SecondaryDark.ForegroundColor ?? theme.SecondaryDark.Color.ContrastingForegroundColor());

            //NB: These are here for backwards compatibility, and will be removed in a future version.
            //These will be removed in version 4.0.0
            SetSolidColorBrush(resourceDictionary, "SecondaryAccentBrush", theme.SecondaryMid.Color);
            SetSolidColorBrush(resourceDictionary, "SecondaryAccentForegroundBrush",
                theme.SecondaryMid.ForegroundColor ?? theme.SecondaryMid.Color.ContrastingForegroundColor());

            SetSolidColorBrush(resourceDictionary, "ValidationErrorBrush", theme.ValidationError);
            resourceDictionary["ValidationErrorColor"] = theme.ValidationError;

            SetSolidColorBrush(resourceDictionary, "MaterialDesignBackground", theme.Background);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignPaper", theme.Paper);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignCardBackground", theme.CardBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignToolBarBackground", theme.ToolBarBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignBody", theme.Body);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignBodyLight", theme.BodyLight);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignColumnHeader", theme.ColumnHeader);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignCheckBoxOff", theme.CheckBoxOff);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignCheckBoxDisabled", theme.CheckBoxDisabled);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignTextBoxBorder", theme.TextBoxBorder);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignDivider", theme.Divider);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignSelection", theme.Selection);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignToolForeground", theme.ToolForeground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignToolBackground", theme.ToolBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignFlatButtonClick", theme.FlatButtonClick);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignFlatButtonRipple", theme.FlatButtonRipple);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignToolTipBackground", theme.ToolTipBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignChipBackground", theme.ChipBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignSnackbarBackground", theme.SnackbarBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignSnackbarMouseOver", theme.SnackbarMouseOver);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignSnackbarRipple", theme.SnackbarRipple);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignTextFieldBoxBackground", theme.TextFieldBoxBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignTextFieldBoxHoverBackground", theme.TextFieldBoxHoverBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignTextFieldBoxDisabledBackground", theme.TextFieldBoxDisabledBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignTextAreaBorder", theme.TextAreaBorder);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignTextAreaInactiveBorder", theme.TextAreaInactiveBorder);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignDataGridRowHoverBackground", theme.DataGridRowHoverBackground);

            if (!(resourceDictionary.GetThemeManager() is ThemeManager themeManager))
                resourceDictionary[ThemeManagerKey] = themeManager = new ThemeManager(resourceDictionary);

            var oldTheme = resourceDictionary.GetTheme();
            resourceDictionary[CurrentThemeKey] = theme;

            themeManager.OnThemeChange(oldTheme, theme);
        }

        public static ITheme GetTheme(this IResourceDictionary resourceDictionary) {
            if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));
            if (resourceDictionary.TryGetResource(CurrentThemeKey, out var theme) && theme is ITheme) return (ITheme) theme;

            var secondaryMid = GetColor("SecondaryHueMidBrush");
            var secondaryMidForeground = GetColor("SecondaryHueMidForegroundBrush");

            if (!TryGetColor("SecondaryHueLightBrush", out var secondaryLight)) secondaryLight = secondaryMid.Lighten();

            if (!TryGetColor("SecondaryHueLightForegroundBrush", out var secondaryLightForeground))
                secondaryLightForeground = secondaryLight.ContrastingForegroundColor();

            if (!TryGetColor("SecondaryHueDarkBrush", out var secondaryDark)) secondaryDark = secondaryMid.Darken();

            if (!TryGetColor("SecondaryHueDarkForegroundBrush", out var secondaryDarkForeground))
                secondaryDarkForeground = secondaryDark.ContrastingForegroundColor();

            //Attempt to simply look up the appropriate resources
            return new Theme {
                PrimaryLight = new ColorPair(GetColor("PrimaryHueLightBrush"), GetColor("PrimaryHueLightForegroundBrush")),
                PrimaryMid = new ColorPair(GetColor("PrimaryHueMidBrush"), GetColor("PrimaryHueMidForegroundBrush")),
                PrimaryDark = new ColorPair(GetColor("PrimaryHueDarkBrush"), GetColor("PrimaryHueDarkForegroundBrush")),

                SecondaryLight = new ColorPair(secondaryLight, secondaryLightForeground),
                SecondaryMid = new ColorPair(secondaryMid, secondaryMidForeground),
                SecondaryDark = new ColorPair(secondaryDark, secondaryDarkForeground),

                Background = GetColor("MaterialDesignBackground"),
                Body = GetColor("MaterialDesignBody"),
                BodyLight = GetColor("MaterialDesignBodyLight"),
                CardBackground = GetColor("MaterialDesignCardBackground"),
                CheckBoxDisabled = GetColor("MaterialDesignCheckBoxDisabled"),
                CheckBoxOff = GetColor("MaterialDesignCheckBoxOff"),
                ChipBackground = GetColor("MaterialDesignChipBackground"),
                ColumnHeader = GetColor("MaterialDesignColumnHeader"),
                DataGridRowHoverBackground = GetColor("MaterialDesignDataGridRowHoverBackground"),
                Divider = GetColor("MaterialDesignDivider"),
                FlatButtonClick = GetColor("MaterialDesignFlatButtonClick"),
                FlatButtonRipple = GetColor("MaterialDesignFlatButtonRipple"),
                Selection = GetColor("MaterialDesignSelection"),
                SnackbarBackground = GetColor("MaterialDesignSnackbarBackground"),
                SnackbarMouseOver = GetColor("MaterialDesignSnackbarMouseOver"),
                SnackbarRipple = GetColor("MaterialDesignSnackbarRipple"),
                TextAreaBorder = GetColor("MaterialDesignTextAreaBorder"),
                TextAreaInactiveBorder = GetColor("MaterialDesignTextAreaInactiveBorder"),
                TextBoxBorder = GetColor("MaterialDesignTextBoxBorder"),
                TextFieldBoxBackground = GetColor("MaterialDesignTextFieldBoxBackground"),
                TextFieldBoxDisabledBackground = GetColor("MaterialDesignTextFieldBoxDisabledBackground"),
                TextFieldBoxHoverBackground = GetColor("MaterialDesignTextFieldBoxHoverBackground"),
                ToolBackground = GetColor("MaterialDesignToolBackground"),
                ToolBarBackground = GetColor("MaterialDesignToolBarBackground"),
                ToolForeground = GetColor("MaterialDesignToolForeground"),
                ToolTipBackground = GetColor("MaterialDesignToolTipBackground"),
                Paper = GetColor("MaterialDesignPaper"),
                ValidationError = GetColor("ValidationErrorBrush")
            };

            Color GetColor(params string[] keys) {
                foreach (var key in keys)
                    if (TryGetColor(key, out var color))
                        return color;

                throw new InvalidOperationException($"Could not locate required resource with key(s) '{string.Join(", ", keys)}'");
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

        [CanBeNull]
        public static IThemeManager GetThemeManager(this IResourceDictionary resourceDictionary) {
            if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));

            return resourceDictionary.TryGetResource(ThemeManagerKey, out var manager) ? manager as IThemeManager : null;
        }

        internal static void SetSolidColorBrush(this IResourceDictionary sourceDictionary, string name, Color value) {
            if (sourceDictionary == null) throw new ArgumentNullException(nameof(sourceDictionary));
            if (name == null) throw new ArgumentNullException(nameof(name));

            if (sourceDictionary.TryGetValue(name + "Color", out var currentValue) && currentValue as Color? == value) return;
            sourceDictionary[name + "Color"] = value;

            if (sourceDictionary.ContainsKey(name) && sourceDictionary[name] is SolidColorBrush brush) {
                Dispatcher.UIThread.InvokeAsync(delegate
                {
                    if (brush.Color == value)
                        return;
                    
                    if (brush.Transitions == null || brush.Transitions.Count == 0)
                    {
                        brush.Transitions = new Transitions
                        {
                            new ColorTransition
                            {
                                Duration = TimeSpan.FromSeconds(0.35), Easing = new SineEaseOut(),
                                Property = SolidColorBrush.ColorProperty
                            }
                        };
                    }
                
                    brush.Color = value;
                });
                
                return;
            }

            var newBrush = new SolidColorBrush(value);
            sourceDictionary[name] = newBrush;
        }

        private class ThemeManager : IThemeManager {
            private readonly IResourceDictionary _resourceDictionary;

            public ThemeManager(IResourceDictionary resourceDictionary) {
                _resourceDictionary = resourceDictionary ?? throw new ArgumentNullException(nameof(resourceDictionary));
            }

            public event EventHandler<ThemeChangedEventArgs> ThemeChanged;

            public void OnThemeChange(ITheme oldTheme, ITheme newTheme) {
                ThemeChanged?.Invoke(this, new ThemeChangedEventArgs(_resourceDictionary, oldTheme, newTheme));
            }
        }
    }
}