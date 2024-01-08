using System;
using Avalonia;
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
    public class MaterialTheme : MaterialThemeBase, IDisposable {
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
        private readonly ITheme _theme = new Theme();

        private BaseThemeMode _actualBaseTheme;
        private bool _isLoaded;
        private IThemeVariantHost? _lastThemeVariantHost;
        private IDisposable? _themeUpdateDisposable;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialTheme"/> class.
        /// </summary>
        /// <param name="serviceProvider">The XAML service provider.</param>
        public MaterialTheme(IServiceProvider serviceProvider) : base(serviceProvider) {
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
            _themeUpdateDisposable?.Dispose();
        }

        private void OnOwnerChanged(object sender, EventArgs e) {
            RegisterActualThemeObservable();
        }

        protected override bool TryGetResource(object key, ThemeVariant? theme, out object? value) {
            return base.TryGetResource(key, theme, out value)
                || base.TryGetResource(key, ActualBaseTheme.GetVariantFromMaterialBaseThemeMode(), out value);
        }

        /// <inheritdoc />
        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change) {
            base.OnPropertyChanged(change);

            if (change.Property == BaseThemeProperty) {
                SetupActualTheme();
                return;
            }

            if (change.Property == ActualBaseThemeProperty) {
                var baseTheme = change.GetNewValue<BaseThemeMode>().GetBaseTheme();
                _theme.SetBaseTheme(baseTheme);
                EnqueueThemeUpdate();
                return;
            }

            if (change.Property == PrimaryColorProperty) {
                var color = change.GetNewValue<PrimaryColor>();
                _theme.SetPrimaryColor(SwatchHelper.Lookup[(MaterialColor)color]);
                EnqueueThemeUpdate();
                return;
            }

            if (change.Property == PrimaryColorProperty) {
                var color = change.GetNewValue<SecondaryColor>();
                _theme.SetPrimaryColor(SwatchHelper.Lookup[(MaterialColor)color]);
                EnqueueThemeUpdate();
                return;
            }
        }

        private void EnqueueThemeUpdate() {
            if (!_isLoaded) return;

            _themeUpdateDisposable?.Dispose();
            _themeUpdateDisposable = DispatcherTimer.RunOnce(() => CurrentTheme = _theme, TimeSpan.FromMilliseconds(100));
        }

        private void RegisterActualThemeObservable() {
            if (_lastThemeVariantHost is not null) _lastThemeVariantHost.ActualThemeVariantChanged -= HostOnActualThemeVariantChanged;

            _lastThemeVariantHost = Owner as IThemeVariantHost;
            if (_lastThemeVariantHost is not null) _lastThemeVariantHost.ActualThemeVariantChanged += HostOnActualThemeVariantChanged;
        }

        private void HostOnActualThemeVariantChanged(object sender, EventArgs e) {
            SetupActualTheme();
        }

        private void SetupActualTheme() {
            var materialBaseThemeModeFromVariant = BaseTheme switch {
                BaseThemeMode.Inherit => (_lastThemeVariantHost?.ActualThemeVariant).GetMaterialBaseThemeModeFromVariant() ?? BaseThemeMode.Light,
                BaseThemeMode.Light   => BaseThemeMode.Light,
                BaseThemeMode.Dark    => BaseThemeMode.Dark,
                _                     => throw new ArgumentOutOfRangeException(nameof(BaseTheme), BaseTheme, null)
            };

            ActualBaseTheme = materialBaseThemeModeFromVariant;
        }

        protected override ITheme ProvideInitialTheme() {
            _isLoaded = true;
            return _theme;
        }
    }
}