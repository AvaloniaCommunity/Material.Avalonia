using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Controls.Presenters;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Threading;

namespace Material.Styles.Themes;

public class MaterialThemeBase : Visual, IStyle, IResourceProvider {
    public static readonly DirectProperty<MaterialThemeBase, IReadOnlyTheme> CurrentThemeProperty =
        AvaloniaProperty.RegisterDirect<MaterialThemeBase, IReadOnlyTheme>(
            nameof(CurrentTheme),
            o => o.CurrentTheme,
            (o, v) => o.CurrentTheme = v);
    private readonly IStyle _compabilityStyles;
    private readonly IStyle _controlsStyles;

    private CancellationTokenSource? _currentCancellationTokenSource;

    private IReadOnlyTheme _currentTheme = new ReadOnlyTheme();
    private Task? _currentThemeUpdateTask;
    private bool _isLoading;
    private IStyle? _loaded;

    static MaterialThemeBase() {
        // Fixes TextBox text color does not change: https://github.com/AvaloniaUI/Avalonia/pull/9631#issuecomment-1353555702
        // Will be fixed in 11.0.0-preview5 apparently
        AffectsRender<TextPresenter>(TextElement.ForegroundProperty);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MaterialThemeBase"/> class.
    /// </summary>
    /// <param name="baseUri">The base URL for the XAML context.</param>
    public MaterialThemeBase(Uri baseUri) {
        _controlsStyles = new StyleInclude(baseUri) { Source = new Uri("avares://Material.Avalonia/Material.Avalonia.Templates.xaml") };
        _compabilityStyles = new StyleInclude(baseUri) { Source = new Uri("avares://Material.Styles/Resources/Compatibility/Index.axaml") };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MaterialThemeBase"/> class.
    /// </summary>
    /// <param name="serviceProvider">The XAML service provider.</param>
    public MaterialThemeBase(IServiceProvider serviceProvider)
        : this(((IUriContext)serviceProvider.GetService(typeof(IUriContext))).BaseUri) { }

    private IResourceDictionary LoadedResourceDictionary => (_loaded as Avalonia.Styling.Styles)!.Resources;

    /// <summary>
    /// Get or set current applied theme
    /// </summary>
    public IReadOnlyTheme CurrentTheme {
        get => _currentTheme;
        set {
            var oldTheme = _currentTheme;
            var newTheme = new ReadOnlyTheme(value);

            if (EqualityComparer<IReadOnlyTheme>.Default.Equals(oldTheme, newTheme))
                return;

            _currentTheme = newTheme;
            SetAndRaise(CurrentThemeProperty, ref _currentTheme, newTheme);
            if (!_isLoading) StartUpdatingTheme(oldTheme, newTheme);
        }
    }

    public IObservable<IReadOnlyTheme> CurrentThemeChanged => this.GetObservable(CurrentThemeProperty);

    public IObservable<MaterialThemeBase> ThemeChangedObservable =>
        Observable.FromEvent<EventHandler, MaterialThemeBase>(
            conversion => delegate(object sender, EventArgs _) {
                if (sender is not MaterialThemeBase theme)
                    return;

                conversion(theme);
            },
            h => ThemeChanged += h,
            h => ThemeChanged -= h);

    /// <summary>
    /// Gets the loaded style.
    /// </summary>
    public IStyle Loaded {
        get {
            if (_loaded != null)
                return _loaded!;

            _isLoading = true;

            _loaded = new Avalonia.Styling.Styles { _controlsStyles, _compabilityStyles };

            var initialTheme = ProvideInitialTheme();
            if (initialTheme != null) {
                UpdateSolidColorBrush(null, initialTheme, LoadedResourceDictionary, InvokeAndReturnTask).Wait();
                CurrentTheme = initialTheme;
            }

            _isLoading = false;

            return _loaded!;

            Task InvokeAndReturnTask(Action action, DispatcherPriority _) {
                action();
                return Task.CompletedTask;
            }
        }
    }

    private static IReadOnlyDictionary<string, Func<IReadOnlyTheme, Color>> UpdatableColors =>
        new Dictionary<string, Func<IReadOnlyTheme, Color>> {
            { "PrimaryHueLightForegroundBrush", theme => theme.PrimaryLight.ForegroundColor },
            { "PrimaryHueMidForegroundBrush", theme => theme.PrimaryMid.ForegroundColor },
            { "PrimaryHueDarkForegroundBrush", theme => theme.PrimaryDark.ForegroundColor },
            { "PrimaryHueLightBrush", theme => theme.PrimaryLight.Color },
            { "PrimaryHueMidBrush", theme => theme.PrimaryMid.Color },
            { "PrimaryHueDarkBrush", theme => theme.PrimaryDark.Color },
            { "SecondaryHueLightForegroundBrush", theme => theme.SecondaryLight.ForegroundColor },
            { "SecondaryHueMidForegroundBrush", theme => theme.SecondaryMid.ForegroundColor },
            { "SecondaryHueDarkForegroundBrush", theme => theme.SecondaryDark.ForegroundColor },
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

    public IResourceHost? Owner => (Loaded as IResourceProvider)?.Owner;

    public event EventHandler? OwnerChanged {
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

    void IResourceProvider.AddOwner(IResourceHost owner) => (Loaded as IResourceProvider)?.AddOwner(owner);
    void IResourceProvider.RemoveOwner(IResourceHost owner) => (Loaded as IResourceProvider)?.RemoveOwner(owner);

    bool IResourceNode.HasResources => (Loaded as IResourceProvider)?.HasResources ?? false;
    public SelectorMatchResult TryAttach(IStyleable target, object? host) => Loaded.TryAttach(target, host);

    IReadOnlyList<IStyle> IStyle.Children => _loaded?.Children ?? Array.Empty<IStyle>();

    public bool TryGetResource(object key, out object? value) {
        if (!_isLoading && Loaded is IResourceProvider p) return p.TryGetResource(key, out value);

        value = null;
        return false;
    }

    /// <summary>
    /// This event is raised when all brushes is changed.
    /// </summary>
    public event EventHandler? ThemeChanged;

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

    private void StartUpdatingTheme(IReadOnlyTheme oldTheme, IReadOnlyTheme newTheme) {
        Task.Run(async () => {
            _currentCancellationTokenSource?.Cancel();
            _currentCancellationTokenSource?.Dispose();

            var currentToken = new CancellationTokenSource();
            _currentCancellationTokenSource = currentToken;

            if (_currentThemeUpdateTask != null) await _currentThemeUpdateTask;
            if (!currentToken.IsCancellationRequested) {
                var task = UpdateSolidColorBrush(oldTheme, newTheme, LoadedResourceDictionary,
                    Dispatcher.UIThread.InvokeAsync);

                _currentThemeUpdateTask = task;

                await task.ContinueWith(delegate {
                    ThemeChanged?.Invoke(this, EventArgs.Empty);
                }, CancellationToken.None);
            }
        });
    }

    private static async Task UpdateSolidColorBrush(IReadOnlyTheme? oldTheme, IReadOnlyTheme newTheme, IResourceDictionary resourceDictionary, Func<Action, DispatcherPriority, Task> contextSync) {
        await Task.WhenAll(UpdatableColors.Select(UpdateColorAsync));
        await FixTextBlockColors();

        Task UpdateColorAsync(KeyValuePair<string, Func<IReadOnlyTheme, Color>> pair) {
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
                    resourceDictionary[pair.Key] = new SolidColorBrush(newColor) { Transitions = new Transitions { new ColorTransition { Duration = TimeSpan.FromSeconds(0.35), Easing = new SineEaseOut(), Property = SolidColorBrush.ColorProperty } } };
                }, DispatcherPriority.Normal);
        }

        // Workaround for TextBlock text color does not changing: https://github.com/AvaloniaUI/Avalonia/issues/9675
        Task FixTextBlockColors() {
            return contextSync(() => {
                var bodyBrush = resourceDictionary["MaterialDesignBody"];
                resourceDictionary.Remove("MaterialDesignBody");
                resourceDictionary.Add("MaterialDesignBody", bodyBrush);
            }, DispatcherPriority.Normal);
        }
    }
}

