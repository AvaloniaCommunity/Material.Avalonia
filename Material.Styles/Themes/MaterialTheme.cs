using System;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Threading;
using Material.Colors;
using Material.Styles.Themes.Base;

namespace Material.Styles.Themes
{
    /// <summary>
    /// Applies the material theme styles and resources
    /// </summary>
    /// <remarks>
    /// You need to setup all these properties: <see cref="BaseTheme"/>, <see cref="PrimaryColor"/>, <see cref="SecondaryColor"/>
    /// </remarks>
    public class MaterialTheme : MaterialThemeBase, IDisposable
    {
        private IDisposable _themeUpdaterDisposable = null!;
        private ITheme _theme = new Theme();

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialTheme"/> class.
        /// </summary>
        /// <param name="baseUri">The base URL for the XAML context.</param>
        public MaterialTheme(Uri baseUri) : base(baseUri)
            => Initialize();

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialTheme"/> class.
        /// </summary>
        /// <param name="serviceProvider">The XAML service provider.</param>
        public MaterialTheme(IServiceProvider serviceProvider) : base(serviceProvider)
            => Initialize();

        private void Initialize()
        {
            var baseThemeObservable = this.GetObservable(BaseThemeProperty)
                .Do(mode => _theme = _theme.SetBaseTheme(mode.GetBaseTheme()))
                .Select(_ => Unit.Default);
            var primaryColorObservable = this.GetObservable(PrimaryColorProperty)
                .Do(color => _theme = _theme.SetPrimaryColor(SwatchHelper.Lookup[(MaterialColor) color]))
                .Select(_ => Unit.Default);
            var secondaryColorObservable = this.GetObservable(SecondaryColorProperty)
                .Do(color => _theme = _theme.SetSecondaryColor(SwatchHelper.Lookup[(MaterialColor) color]))
                .Select(_ => Unit.Default);

            _themeUpdaterDisposable = baseThemeObservable
                .Merge(primaryColorObservable)
                .Merge(secondaryColorObservable)
                .Where(_ => _isLoaded)
                .Throttle(TimeSpan.FromMilliseconds(100))
                .ObserveOn(new AvaloniaSynchronizationContext())
                .Subscribe(_ => CurrentTheme = _theme);
        }

        private bool _isLoaded;
        protected override ITheme? ProvideInitialTheme() {
            _isLoaded = true;
            return _theme;
        }

        public static readonly StyledProperty<BaseThemeMode> BaseThemeProperty
            = AvaloniaProperty.Register<MaterialTheme, BaseThemeMode>(nameof(BaseTheme));

        public BaseThemeMode BaseTheme
        {
            get => GetValue(BaseThemeProperty);
            set => SetValue(BaseThemeProperty, value);
        }

        public static readonly StyledProperty<PrimaryColor> PrimaryColorProperty
            = AvaloniaProperty.Register<MaterialTheme, PrimaryColor>(nameof(PrimaryColor));

        public PrimaryColor PrimaryColor
        {
            get => GetValue(PrimaryColorProperty);
            set => SetValue(PrimaryColorProperty, value);
        }

        public static readonly StyledProperty<SecondaryColor> SecondaryColorProperty
            = AvaloniaProperty.Register<MaterialTheme, SecondaryColor>(nameof(SecondaryColor));

        public SecondaryColor SecondaryColor
        {
            get => GetValue(SecondaryColorProperty);
            set => SetValue(SecondaryColorProperty, value);
        }

        public void Dispose()
        {
            _themeUpdaterDisposable.Dispose();
        }
    }
}