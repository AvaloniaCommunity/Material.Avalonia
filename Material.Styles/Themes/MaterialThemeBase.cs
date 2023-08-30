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
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Threading;

namespace Material.Styles.Themes;

public class MaterialThemeBase : Avalonia.Styling.Styles, IResourceNode {
    public static readonly DirectProperty<MaterialThemeBase, IReadOnlyTheme> CurrentThemeProperty =
        AvaloniaProperty.RegisterDirect<MaterialThemeBase, IReadOnlyTheme>(
            nameof(CurrentTheme),
            o => o.CurrentTheme,
            (o, v) => o.CurrentTheme = v);

    private CancellationTokenSource? _currentCancellationTokenSource;

    private IReadOnlyTheme _currentTheme = new ReadOnlyTheme();
    private Task? _currentThemeUpdateTask;
    private bool _isResourcedAccessed;

    /// <summary>
    /// Initializes a new instance of the <see cref="MaterialThemeBase"/> class.
    /// </summary>
    /// <param name="serviceProvider">The parent's service provider.</param>
    public MaterialThemeBase(IServiceProvider? serviceProvider) {
        AvaloniaXamlLoader.Load(serviceProvider, this);
    }

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
            StartUpdatingTheme(oldTheme, newTheme);
        }
    }

    public IObservable<IReadOnlyTheme> CurrentThemeChanged => this.GetObservable(CurrentThemeProperty);

    public IObservable<MaterialThemeBase> ThemeChangedEndObservable =>
        Observable.FromEvent<EventHandler, MaterialThemeBase>(
            conversion => delegate(object sender, EventArgs _) {
                if (sender is not MaterialThemeBase theme)
                    return;

                conversion(theme);
            },
            h => ThemeChangedEnd += h,
            h => ThemeChangedEnd -= h);

    private static IReadOnlyDictionary<string, Func<IReadOnlyTheme, Color>> UpdatableColors =>
        new Dictionary<string, Func<IReadOnlyTheme, Color>> {
            { "MaterialPrimaryHueLightForegroundBrush", theme => theme.PrimaryLight.ForegroundColor },
            { "MaterialPrimaryHueMidForegroundBrush", theme => theme.PrimaryMid.ForegroundColor },
            { "MaterialPrimaryHueDarkForegroundBrush", theme => theme.PrimaryDark.ForegroundColor },
            { "MaterialPrimaryHueLightBrush", theme => theme.PrimaryLight.Color },
            { "MaterialPrimaryHueMidBrush", theme => theme.PrimaryMid.Color },
            { "MaterialPrimaryHueDarkBrush", theme => theme.PrimaryDark.Color },
            { "MaterialSecondaryHueLightForegroundBrush", theme => theme.SecondaryLight.ForegroundColor },
            { "MaterialSecondaryHueMidForegroundBrush", theme => theme.SecondaryMid.ForegroundColor },
            { "MaterialSecondaryHueDarkForegroundBrush", theme => theme.SecondaryDark.ForegroundColor },
            { "MaterialSecondaryHueLightBrush", theme => theme.SecondaryLight.Color },
            { "MaterialSecondaryHueMidBrush", theme => theme.SecondaryMid.Color },
            { "MaterialSecondaryHueDarkBrush", theme => theme.SecondaryDark.Color },
            { "MaterialValidationErrorBrush", theme => theme.ValidationError },
            { "MaterialBackgroundBrush", theme => theme.Background },
            { "MaterialPaperBrush", theme => theme.Paper },
            { "MaterialCardBackgroundBrush", theme => theme.CardBackground },
            { "MaterialToolBarBackgroundBrush", theme => theme.ToolBarBackground },
            { "MaterialBodyBrush", theme => theme.Body },
            { "MaterialBodyLightBrush", theme => theme.BodyLight },
            { "MaterialColumnHeaderBrush", theme => theme.ColumnHeader },
            { "MaterialCheckBoxOffBrush", theme => theme.CheckBoxOff },
            { "MaterialCheckBoxDisabledBrush", theme => theme.CheckBoxDisabled },
            { "MaterialTextBoxBorderBrush", theme => theme.TextBoxBorder },
            { "MaterialDividerBrush", theme => theme.Divider },
            { "MaterialSelectionBrush", theme => theme.Selection },
            { "MaterialToolForegroundBrush", theme => theme.ToolForeground },
            { "MaterialToolBackgroundBrush", theme => theme.ToolBackground },
            { "MaterialFlatButtonClickBrush", theme => theme.FlatButtonClick },
            { "MaterialFlatButtonRippleBrush", theme => theme.FlatButtonRipple },
            { "MaterialToolTipBackgroundBrush", theme => theme.ToolTipBackground },
            { "MaterialChipBackgroundBrush", theme => theme.ChipBackground },
            { "MaterialSnackbarBackgroundBrush", theme => theme.SnackbarBackground },
            { "MaterialSnackbarMouseOverBrush", theme => theme.SnackbarMouseOver },
            { "MaterialSnackbarRippleBrush", theme => theme.SnackbarRipple },
            { "MaterialTextFieldBoxBackgroundBrush", theme => theme.TextFieldBoxBackground },
            { "MaterialTextFieldBoxHoverBackgroundBrush", theme => theme.TextFieldBoxHoverBackground },
            { "MaterialTextFieldBoxDisabledBackgroundBrush", theme => theme.TextFieldBoxDisabledBackground },
            { "MaterialTextAreaBorderBrush", theme => theme.TextAreaBorder },
            { "MaterialDesignTextAreaInactiveBorder", theme => theme.TextAreaInactiveBorder },
            { "MaterialDesignDataGridRowHoverBackground", theme => theme.DataGridRowHoverBackground },
        };

    bool IResourceNode.TryGetResource(object key, ThemeVariant? theme, out object? value) {
        if (!_isResourcedAccessed) {
            _isResourcedAccessed = true;
            OnResourcedAccessed();
        }

        return base.TryGetResource(key, theme, out value);
    }

    /// <summary>
    /// This event is raised when all brushes is changed.
    /// </summary>
    public event EventHandler? ThemeChangedEnd;

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

    private void OnResourcedAccessed() {
        var initialTheme = ProvideInitialTheme();
        if (initialTheme != null) {
            var newTheme = new ReadOnlyTheme(initialTheme);
            var defaultThemeDictionary = (ResourceDictionary)Resources.ThemeDictionaries[ThemeVariant.Default];
            UpdateSolidColorBrush(null, newTheme, defaultThemeDictionary, InvokeImmediately).Wait();
            var oldTheme = _currentTheme;
            _currentTheme = newTheme;
            RaisePropertyChanged(CurrentThemeProperty, oldTheme, newTheme);
        }
    }

    private void StartUpdatingTheme(IReadOnlyTheme oldTheme, IReadOnlyTheme newTheme) {
        Task.Run(async () => {
            _currentCancellationTokenSource?.Cancel();
            _currentCancellationTokenSource?.Dispose();

            var currentToken = new CancellationTokenSource();
            _currentCancellationTokenSource = currentToken;

            if (_currentThemeUpdateTask != null) await _currentThemeUpdateTask;
            if (!currentToken.IsCancellationRequested) {
                // If control is not attached to visual tree (is doesn't have Parent)
                // And we inside a dispatcher thread (since it required for SolidColorBrush creation/changing)
                // We can just invoke all color changes RIGHT NOW ON CURRENT THREAD
                // -------------------------------------------------------------------
                // If we already attached to something (e.g. theme was changed while app is running)
                // We enqueue everything to dispatcher thread
                // Cuz if we execute everything RIGHT NOW on dispatcher thread it will cause lag spike
                // So we changing colors one by one
                Func<Action, DispatcherPriority, Task> contextSync = Owner is null && Dispatcher.UIThread.CheckAccess()
                    ? InvokeImmediately
                    : (action, priority) => Dispatcher.UIThread.InvokeAsync(action, priority).GetTask();
                var defaultThemeDictionary = (ResourceDictionary)Resources.ThemeDictionaries[ThemeVariant.Default];
                var task = UpdateSolidColorBrush(oldTheme, newTheme, defaultThemeDictionary, contextSync);

                _currentThemeUpdateTask = task;

                await task.ContinueWith(delegate {
                    ThemeChangedEnd?.Invoke(this, EventArgs.Empty);
                }, CancellationToken.None);
            }
        });
    }

    private static Task UpdateSolidColorBrush(IReadOnlyTheme? oldTheme, IReadOnlyTheme newTheme, IResourceDictionary resourceDictionary, Func<Action, DispatcherPriority, Task> contextSync) {
        return Task.WhenAll(UpdatableColors.Select(UpdateColorAsync));

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
    }

    private Task InvokeImmediately(Action action, DispatcherPriority priority = default) {
        action();
        return Task.CompletedTask;
    }
}
