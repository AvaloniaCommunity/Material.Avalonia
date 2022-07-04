using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using Avalonia.Threading;
using Material.Colors.ColorManipulation;

namespace Material.Styles.Themes {
    public class MaterialThemeBase : AvaloniaObject, IStyle, IResourceProvider {
        private readonly IStyle _controlsStyles;
        private bool _isLoading;
        private IStyle? _loaded;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentTheme"/> class.
        /// </summary>
        /// <param name="baseUri">The base URL for the XAML context.</param>
        public MaterialThemeBase(Uri baseUri) {
            _controlsStyles = new StyleInclude(baseUri) {
                Source = new Uri("avares://Material.Avalonia/Material.Avalonia.Templates.xaml")
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentTheme"/> class.
        /// </summary>
        /// <param name="serviceProvider">The XAML service provider.</param>
        public MaterialThemeBase(IServiceProvider serviceProvider)
            : this(((IUriContext)serviceProvider.GetService(typeof(IUriContext))).BaseUri) { }

        private IResourceDictionary LoadedResourceDictionary => (_loaded as Avalonia.Styling.Styles)!.Resources;

        public static readonly DirectProperty<MaterialThemeBase, ITheme> CurrentThemeProperty =
            AvaloniaProperty.RegisterDirect<MaterialThemeBase, ITheme>(
                nameof(CurrentTheme),
                o => o.CurrentTheme,
                (o, v) => o.CurrentTheme = v);

        private ThemeStruct _currentTheme = new();

        /// <summary>
        /// Get or set current applied theme
        /// </summary>
        /// <returns>
        /// Returns a STRUCT implementing ITheme interface 
        /// </returns>
        public ITheme CurrentTheme {
            get => new ThemeStruct(_currentTheme);
            set {
                var oldTheme = _currentTheme;
                var newTheme = new ThemeStruct(value);

                if (EqualityComparer<ITheme>.Default.Equals(oldTheme, newTheme)) return;
                _currentTheme = newTheme;
                RaisePropertyChanged(CurrentThemeProperty, oldTheme, newTheme);
                if (!_isLoading) StartUpdatingTheme(oldTheme, newTheme);
            }
        }

        public IObservable<ITheme> CurrentThemeChanged => this.GetObservable(CurrentThemeProperty);

        public IResourceHost? Owner => (Loaded as IResourceProvider)?.Owner;

        /// <summary>
        /// Gets the loaded style.
        /// </summary>
        public IStyle Loaded {
            get {
                if (_loaded == null) {
                    _isLoading = true;

                    _loaded = new Avalonia.Styling.Styles() { _controlsStyles };

                    var initialTheme = ProvideInitialTheme();
                    if (initialTheme != null) {
                        UpdateSolidColorBrush(null, initialTheme, LoadedResourceDictionary, InvokeAndReturnTask).Wait();
                        CurrentTheme = initialTheme;
                    }

                    _isLoading = false;
                }

                return _loaded!;

                Task InvokeAndReturnTask(Action action, DispatcherPriority _) {
                    action();
                    return Task.CompletedTask;
                }
            }
        }

        bool IResourceNode.HasResources => (Loaded as IResourceProvider)?.HasResources ?? false;
        IReadOnlyList<IStyle> IStyle.Children => _loaded?.Children ?? Array.Empty<IStyle>();

        public event EventHandler OwnerChanged {
            add {
                if (Loaded is IResourceProvider rp) {
                    rp.OwnerChanged += value;
                }
            }
            remove {
                if (Loaded is IResourceProvider rp) {
                    rp.OwnerChanged -= value;
                }
            }
        }

        public SelectorMatchResult TryAttach(IStyleable target, IStyleHost? host) => Loaded.TryAttach(target, host);
        public bool TryGetResource(object key, out object? value) {
            if (!_isLoading && Loaded is IResourceProvider p) {
                return p.TryGetResource(key, out value);
            }

            value = null;
            return false;
        }
        void IResourceProvider.AddOwner(IResourceHost owner) => (Loaded as IResourceProvider)?.AddOwner(owner);
        void IResourceProvider.RemoveOwner(IResourceHost owner) => (Loaded as IResourceProvider)?.RemoveOwner(owner);

        /// <summary>
        /// This method will be called to get the theme that will be applied at the start of the application. 
        /// </summary>
        /// <remarks>
        /// All elements specified in the App.xaml are already initialized, all properties specified in the markup are assigned.
        /// At this point, avalonia begins to collect and initialize styles and resources.
        /// </remarks>
        /// <returns>
        /// The theme that will be applied initially. <c>null</c> if the theme does not need to be applied initially.
        /// </returns>
        protected virtual ITheme? ProvideInitialTheme() {
            return null;
        }

        private CancellationTokenSource? _currentCancellationTokenSource;
        private Task? _currentThemeUpdateTask;
        private void StartUpdatingTheme(ITheme oldTheme, ITheme newTheme) => Task.Run(async () => {
            _currentCancellationTokenSource?.Cancel();
            _currentCancellationTokenSource?.Dispose();

            var currentToken = new CancellationTokenSource();
            _currentCancellationTokenSource = currentToken;

            if (_currentThemeUpdateTask != null) await _currentThemeUpdateTask;
            if (!currentToken.IsCancellationRequested) {
                _currentThemeUpdateTask = UpdateSolidColorBrush(oldTheme, newTheme, LoadedResourceDictionary, Dispatcher.UIThread.InvokeAsync);
            }
        });

        private static IReadOnlyDictionary<string, Func<ITheme, Color>> UpdatableColors =>
            new Dictionary<string, Func<ITheme, Color>>() {
                { "PrimaryHueLightForegroundBrush", theme => theme.PrimaryLight.ForegroundColor ?? theme.PrimaryLight.Color.ContrastingForegroundColor() },
                { "PrimaryHueMidForegroundBrush", theme => theme.PrimaryMid.ForegroundColor ?? theme.PrimaryMid.Color.ContrastingForegroundColor() },
                { "PrimaryHueDarkForegroundBrush", theme => theme.PrimaryDark.ForegroundColor ?? theme.PrimaryDark.Color.ContrastingForegroundColor() },
                { "PrimaryHueLightBrush", theme => theme.PrimaryLight.Color },
                { "PrimaryHueMidBrush", theme => theme.PrimaryMid.Color },
                { "PrimaryHueDarkBrush", theme => theme.PrimaryDark.Color },
                { "SecondaryHueLightForegroundBrush", theme => theme.SecondaryLight.ForegroundColor ?? theme.SecondaryLight.Color.ContrastingForegroundColor() },
                { "SecondaryHueMidForegroundBrush", theme => theme.SecondaryMid.ForegroundColor ?? theme.SecondaryMid.Color.ContrastingForegroundColor() },
                { "SecondaryHueDarkForegroundBrush", theme => theme.SecondaryDark.ForegroundColor ?? theme.SecondaryDark.Color.ContrastingForegroundColor() },
                { "SecondaryHueLightBrush", theme => theme.SecondaryLight.Color },
                { "SecondaryHueMidBrush", theme => theme.SecondaryMid.Color },
                { "SecondaryHueDarkBrush", theme => theme.SecondaryDark.Color },
                { "ValidationErrorBrush", theme => theme.ValidationError },
                { "MaterialDesignBackground", theme => theme.Background },
                { "MaterialDesignPaper", theme => theme.Paper },
                { "MaterialDesignCardBackground", theme => theme.CardBackground },
                { "MaterialDesignToolBarBackground", theme => theme.ToolBarBackground },
                { "MaterialDesignBody", theme => theme.Body },
                { "MaterialDesignBodyLight", theme => theme.BodyLight },
                { "MaterialDesignColumnHeader", theme => theme.ColumnHeader },
                { "MaterialDesignCheckBoxOff", theme => theme.CheckBoxOff },
                { "MaterialDesignCheckBoxDisabled", theme => theme.CheckBoxDisabled },
                { "MaterialDesignTextBoxBorder", theme => theme.TextBoxBorder },
                { "MaterialDesignDivider", theme => theme.Divider },
                { "MaterialDesignSelection", theme => theme.Selection },
                { "MaterialDesignToolForeground", theme => theme.ToolForeground },
                { "MaterialDesignToolBackground", theme => theme.ToolBackground },
                { "MaterialDesignFlatButtonClick", theme => theme.FlatButtonClick },
                { "MaterialDesignFlatButtonRipple", theme => theme.FlatButtonRipple },
                { "MaterialDesignToolTipBackground", theme => theme.ToolTipBackground },
                { "MaterialDesignChipBackground", theme => theme.ChipBackground },
                { "MaterialDesignSnackbarBackground", theme => theme.SnackbarBackground },
                { "MaterialDesignSnackbarMouseOver", theme => theme.SnackbarMouseOver },
                { "MaterialDesignSnackbarRipple", theme => theme.SnackbarRipple },
                { "MaterialDesignTextFieldBoxBackground", theme => theme.TextFieldBoxBackground },
                { "MaterialDesignTextFieldBoxHoverBackground", theme => theme.TextFieldBoxHoverBackground },
                { "MaterialDesignTextFieldBoxDisabledBackground", theme => theme.TextFieldBoxDisabledBackground },
                { "MaterialDesignTextAreaBorder", theme => theme.TextAreaBorder },
                { "MaterialDesignTextAreaInactiveBorder", theme => theme.TextAreaInactiveBorder },
                { "MaterialDesignDataGridRowHoverBackground", theme => theme.DataGridRowHoverBackground },
            };

        private static Task UpdateSolidColorBrush(ITheme? oldTheme, ITheme newTheme, IResourceDictionary resourceDictionary, Func<Action, DispatcherPriority, Task> contextSync) {
            return Task.WhenAll(UpdatableColors.Select(UpdateColorAsync).ToArray());

            Task UpdateColorAsync(KeyValuePair<string, Func<ITheme, Color>> pair) {
                var oldColor = oldTheme != null ? pair.Value(oldTheme) : (Color?)null;
                var newColor = pair.Value(newTheme);

                if (oldColor == newColor) return Task.CompletedTask;
                return oldColor != null && resourceDictionary.TryGetValue(pair.Key, out var b) && b is SolidColorBrush brush
                    ? UpdateBrushAsync()
                    : CreateBrushAsync();

                Task UpdateBrushAsync()
                    => contextSync(() => {
                        brush.Color = newColor;
                    }, DispatcherPriority.Normal);

                Task CreateBrushAsync()
                    => contextSync(() => {
                        resourceDictionary[pair.Key] = new SolidColorBrush(newColor) {
                            Transitions = new Transitions {
                                new ColorTransition {
                                    Duration = TimeSpan.FromSeconds(0.35), Easing = new SineEaseOut(), Property = SolidColorBrush.ColorProperty
                                }
                            }
                        };
                    }, DispatcherPriority.Normal);
            }
        }
    }
}