using System;
using Avalonia;
using Avalonia.Themes.Fluent;
using Material.Colors;
using Material.Styles.Themes.Base;

namespace Material.Styles.Themes {
    /// <summary>
    /// Applies the material theme styles and resources
    /// </summary>
    public class MaterialTheme : MaterialThemeBase {
        /// <summary>
        /// Initializes a new instance of the <see cref="FluentTheme"/> class.
        /// </summary>
        /// <param name="baseUri">The base URL for the XAML context.</param>
        public MaterialTheme(Uri baseUri) : base(baseUri) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentTheme"/> class.
        /// </summary>
        /// <param name="serviceProvider">The XAML service provider.</param>
        public MaterialTheme(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public static readonly StyledProperty<BaseThemeMode> BaseThemeProperty
            = AvaloniaProperty.Register<MaterialTheme, BaseThemeMode>(nameof(BaseTheme));

        public BaseThemeMode BaseTheme {
            get => GetValue(BaseThemeProperty);
            set => SetValue(BaseThemeProperty, value);
        }

        public static readonly StyledProperty<PrimaryColor> PrimaryColorProperty
            = AvaloniaProperty.Register<MaterialTheme, PrimaryColor>(nameof(PrimaryColor));

        public PrimaryColor PrimaryColor {
            get => GetValue(PrimaryColorProperty);
            set => SetValue(PrimaryColorProperty, value);
        }

        public static readonly StyledProperty<SecondaryColor> SecondaryColorProperty
            = AvaloniaProperty.Register<MaterialTheme, SecondaryColor>(nameof(SecondaryColor));

        public SecondaryColor SecondaryColor {
            get => GetValue(SecondaryColorProperty);
            set => SetValue(SecondaryColorProperty, value);
        }

        protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change) {
            base.OnPropertyChanged(change);
            if (change.Property == BaseThemeProperty) {
                CurrentTheme.SetBaseTheme(BaseTheme.GetBaseTheme());
                _ = ApplyCurrentThemeAsync();
            }
            if (change.Property == PrimaryColorProperty) {
                CurrentTheme.SetPrimaryColor(SwatchHelper.Lookup[(MaterialColor)PrimaryColor]);
                _ = ApplyCurrentThemeAsync();
            }
            if (change.Property == SecondaryColorProperty) {
                CurrentTheme.SetSecondaryColor(SwatchHelper.Lookup[(MaterialColor)SecondaryColor]);
                _ = ApplyCurrentThemeAsync();
            }
        }
    }
}