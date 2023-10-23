using System;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
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
    public class MaterialTheme : MaterialThemeBase, IDisposable, IResourceNode {
        public static readonly StyledProperty<BaseThemeMode> BaseThemeProperty =
            AvaloniaProperty.Register<MaterialTheme, BaseThemeMode>(nameof(BaseTheme));

        public static readonly StyledProperty<PrimaryColor> PrimaryColorProperty =
            AvaloniaProperty.Register<MaterialTheme, PrimaryColor>(nameof(PrimaryColor));

        public static readonly StyledProperty<SecondaryColor> SecondaryColorProperty =
            AvaloniaProperty.Register<MaterialTheme, SecondaryColor>(nameof(SecondaryColor));

        public static readonly DirectProperty<MaterialTheme, BaseThemeMode> ActualBaseThemeProperty =
            AvaloniaProperty.RegisterDirect<MaterialTheme, BaseThemeMode>(
                nameof(ActualBaseTheme),
                o => o.ActualBaseTheme);
        private readonly IDisposable _themeUpdaterDisposable;

        private BaseThemeMode _actualBaseTheme;
        private IDisposable? _baseThemeChangeObservable;
        private bool _isLoaded;
        private ITheme _theme = new Theme();

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialTheme"/> class.
        /// </summary>
        /// <param name="serviceProvider">The XAML service provider.</param>
        public MaterialTheme(IServiceProvider serviceProvider) : base(serviceProvider) {
            var baseThemeObservable = this.GetObservable(ActualBaseThemeProperty)
                .Do(mode => _theme = _theme.SetBaseTheme(mode.GetBaseTheme()))
                .Select(_ => Unit.Default);
            var primaryColorObservable = this.GetObservable(PrimaryColorProperty)
                .Do(color => _theme = _theme.SetPrimaryColor(SwatchHelper.Lookup[(MaterialColor)color]))
                .Select(_ => Unit.Default);
            var secondaryColorObservable = this.GetObservable(SecondaryColorProperty)
                .Do(color => _theme = _theme.SetSecondaryColor(SwatchHelper.Lookup[(MaterialColor)color]))
                .Select(_ => Unit.Default);

            _themeUpdaterDisposable = baseThemeObservable
                .Merge(primaryColorObservable)
                .Merge(secondaryColorObservable)
                .Where(_ => _isLoaded)
                .Throttle(TimeSpan.FromMilliseconds(100))
                .ObserveOn(new AvaloniaSynchronizationContext())
                .Subscribe(_ => CurrentTheme = _theme);

            OwnerChanged += OnOwnerChanged;
        }

        public BaseThemeMode BaseTheme {
            get => GetValue(BaseThemeProperty);
            set => SetValue(BaseThemeProperty, value);
        }

        public BaseThemeMode ActualBaseTheme {
            get => _actualBaseTheme;
            private set => SetAndRaise(ActualBaseThemeProperty, ref _actualBaseTheme, value);
        }

        public PrimaryColor PrimaryColor {
            get => GetValue(PrimaryColorProperty);
            set => SetValue(PrimaryColorProperty, value);
        }

        public SecondaryColor SecondaryColor {
            get => GetValue(SecondaryColorProperty);
            set => SetValue(SecondaryColorProperty, value);
        }

        public void Dispose() {
            _themeUpdaterDisposable.Dispose();
        }

        private void OnOwnerChanged(object sender, EventArgs e) {
            RegisterActualThemeObservable();
        }

        protected override bool TryGetResource(object key, ThemeVariant? theme, out object? value) {
            return base.TryGetResource(key, theme, out value)
                || base.TryGetResource(key, ActualBaseTheme.GetVariantFromMaterialBaseThemeMode(), out value);
        }

        private void RegisterActualThemeObservable() {
            _baseThemeChangeObservable?.Dispose();

            var themeVariantHost = Owner as IThemeVariantHost;
            var themeVariantObservable = themeVariantHost != null
                ? Observable.FromEvent<EventHandler, Unit>(action => (_, _) => { action(Unit.Default); },
                        handler => themeVariantHost.ActualThemeVariantChanged += handler,
                        handler => themeVariantHost.ActualThemeVariantChanged -= handler)
                    .Select(_ => Unit.Default)
                : Observable.Empty<Unit>();

            var targetBaseObservable = this.GetObservable(BaseThemeProperty)
                .Select(_ => Unit.Default);

            _baseThemeChangeObservable = Observable.Return(Unit.Default)
                .Merge(themeVariantObservable)
                .Merge(targetBaseObservable)
                .Subscribe(_ => {
                    ActualBaseTheme = GetActualBaseTheme(BaseTheme, themeVariantHost?.ActualThemeVariant);
                });
        }

        private BaseThemeMode GetActualBaseTheme(BaseThemeMode mode, ThemeVariant? variant) {
            return mode switch {
                BaseThemeMode.Inherit => variant.GetMaterialBaseThemeModeFromVariant() ?? BaseThemeMode.Light,
                BaseThemeMode.Light   => BaseThemeMode.Light,
                BaseThemeMode.Dark    => BaseThemeMode.Dark,
                _                     => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };
        }

        protected override ITheme ProvideInitialTheme() {
            _isLoaded = true;
            return _theme;
        }
    }
}