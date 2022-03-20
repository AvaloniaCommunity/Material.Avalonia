using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Avalonia;
using Avalonia.Themes.Fluent;
using Avalonia.Threading;
using Material.Colors;
using Material.Styles.Themes.Base;

namespace Material.Styles.Themes {
    /// <summary>
    /// Applies the material theme styles and resources
    /// </summary>
    /// <remarks>
    /// You need to setup all these properties: <see cref="BaseTheme"/>, <see cref="PrimaryColor"/>, <see cref="SecondaryColor"/>
    /// </remarks>
    public class MaterialTheme : MaterialThemeBase, IDisposable {
        private IDisposable _themeUpdaterDisposable = null!;
        /// <summary>
        /// Initializes a new instance of the <see cref="FluentTheme"/> class.
        /// </summary>
        /// <param name="baseUri">The base URL for the XAML context.</param>
        public MaterialTheme(Uri baseUri) : base(baseUri) 
            => Initialize();

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentTheme"/> class.
        /// </summary>
        /// <param name="serviceProvider">The XAML service provider.</param>
        public MaterialTheme(IServiceProvider serviceProvider) : base(serviceProvider) 
            => Initialize();

        private void Initialize() {
            var baseThemeObservable = this.GetObservable(BaseThemeProperty).Select(mode => Unit.Default);
            var primaryColorObservable = this.GetObservable(PrimaryColorProperty).Select(mode => Unit.Default);
            var secondaryColorObservable = this.GetObservable(SecondaryColorProperty).Select(mode => Unit.Default);

            _themeUpdaterDisposable = baseThemeObservable
                .Merge(primaryColorObservable)
                .Merge(secondaryColorObservable)
                .Throttle(TimeSpan.FromMilliseconds(100))
                .ObserveOn(new AvaloniaSynchronizationContext())
                .Subscribe(_ => CurrentTheme = _theme);
        }

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

        private bool _isBaseThemePropertyApplied;
        private bool _isPrimaryColorPropertyApplied;
        private bool _isSecondaryColorPropertyApplied;
        private ITheme _theme = new ThemeStruct();
        protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change) {
            base.OnPropertyChanged(change);
            if (change.Property == BaseThemeProperty) {
                _theme = _theme.SetBaseTheme(BaseTheme.GetBaseTheme());
                _isBaseThemePropertyApplied = true;
                TryApplyTheme();
            }
            if (change.Property == PrimaryColorProperty) {
                _theme = _theme.SetPrimaryColor(SwatchHelper.Lookup[(MaterialColor)PrimaryColor]);
                _isPrimaryColorPropertyApplied = true;
                TryApplyTheme();
            }
            if (change.Property == SecondaryColorProperty) {
                _theme = _theme.SetSecondaryColor(SwatchHelper.Lookup[(MaterialColor)SecondaryColor]);
                _isSecondaryColorPropertyApplied = true;
                TryApplyTheme();
            }

            void TryApplyTheme() {
                if (_isBaseThemePropertyApplied && _isPrimaryColorPropertyApplied && _isSecondaryColorPropertyApplied) {
                    // _themeChangedSubject.OnNext(_theme);
                }
            }
        }
        public void Dispose() {
            _themeUpdaterDisposable.Dispose();
        }
    }
}