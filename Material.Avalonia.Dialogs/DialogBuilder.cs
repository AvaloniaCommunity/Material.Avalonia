using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Styling;
using Material.Dialog.Icons;
using Material.Dialog.ViewModels;
using Material.Dialog.ViewModels.Elements;
using Material.Dialog.ViewModels.Elements.Header;
using Material.Dialog.ViewModels.Elements.Header.Icons;
using Material.Dialog.Views;
using Material.Styles.Controls;

namespace Material.Dialog;

/// <summary>
/// Dialog builder API, which used to compose dialog elements and build them as a ready-for-use control, or a standalone window.
/// </summary>
public class DialogBuilder {
    private IconViewModelBase? _titleView;
    private string _titleText = "Warning";
    private readonly List<object> _elementSet = new();
    private readonly List<IStyle> _styleSet = new();
    private readonly List<DialogBuilderButtonViewModel> _positiveButtons = new();
    private readonly List<DialogBuilderButtonViewModel> _neutralButtons = new();
    private readonly List<DialogBuilderButtonViewModel> _negativeButtons = new();

    /// <summary>
    /// Set dialog content title text
    /// </summary>
    /// <param name="text">title text</param>
    public DialogBuilder SetTitle(string text) {
        _titleText = text;
        return this;
    }

    /// <summary>
    /// Set title icon with integrated coloured icon (not appending!, you can use custom control if you want do that.)
    /// </summary>
    /// <param name="icon">pick one icon that you want to use</param>
    public DialogBuilder SetTitleIcon(DialogIconKind icon) {
        _titleView = new DialogIconViewModel {
            Kind = icon
        };
        return this;
    }

    /// <summary>
    /// Set title icon with bitmap object.
    /// </summary>
    public DialogBuilder SetTitleIcon(Bitmap bitmap, Stretch stretch = Stretch.Uniform) {
        _titleView = new ImageIconViewModel {
            Bitmap = bitmap,
            Stretch = stretch
        };
        return this;
    }

    /// <summary>
    /// Set title icon with customised user control instance for complex usage.
    /// </summary>
    /// <param name="control">non-null control instance</param>
    public DialogBuilder SetTitleIcon(Control control) {
        _titleView = new ControlIconViewModel {
            Control = control
        };
        return this;
    }

    /// <summary>
    /// Append supporting text to dialog.
    /// </summary>
    /// <param name="text">supporting text</param>
    public DialogBuilder Text(string text) {
        _elementSet.Add(new TextBlockElement {
            Text = text
        });
        return this;
    }

    /// <summary>
    /// Append custom control to dialog.
    /// </summary>
    /// <param name="control">Valid control</param>
    public DialogBuilder Control(Control control) {
        _elementSet.Add(control);

        return this;
    }

    /// <summary>
    /// Add a button that close dialog with positive result, also it would take higher priority (sorting)
    /// </summary>
    /// <param name="content">text or control</param>
    /// <param name="returnValue">object that you would want to use as a result</param>
    /// <param name="shouldClose">you can use this parameter to blocking dialog get closed by some conditions</param>
    public DialogBuilder PositiveButton(object content, object returnValue, Func<bool>? shouldClose = null) {
        _positiveButtons.Add(new() {
            Content = content,
            ReturnValue = returnValue,
            ShouldClose = shouldClose
        });

        return this;
    }

    /// <summary>
    /// Add a button that won't close dialog, only providing dialog state.
    /// </summary>
    /// <param name="content">text or control</param>
    /// <param name="returnValue">object that you would want to use as a result</param>
    /// <param name="handler">on click button handler</param>
    public DialogBuilder NeutralButton(object content, object returnValue, Action<object>? handler = null) {
        _neutralButtons.Add(new() {
            Content = content,
            ReturnValue = returnValue,
            ShouldClose = () => {
                handler?.Invoke(returnValue);
                return false;
            }
        });

        return this;
    }

    /// <summary>
    /// Add a button that close dialog with negative result, also it would lower priority (sorting)
    /// </summary>
    /// <param name="content">text or control</param>
    /// <param name="returnValue">object that you would want to use as a result</param>
    /// <param name="shouldClose">you can use this parameter to blocking dialog get closed by some conditions</param>
    public DialogBuilder NegativeButton(object content, object returnValue, Func<bool>? shouldClose = null) {
        _negativeButtons.Add(new() {
            Content = content,
            ReturnValue = returnValue,
            ShouldClose = shouldClose
        });

        return this;
    }

    /// <summary>
    /// Add custom style to dialog for affecting decoration or visual style of controls or elements of dialog
    /// </summary>
    /// <param name="style">valid style instance</param>
    public DialogBuilder Style(IStyle style) {
        _styleSet.Add(style);
        return this;
    }

    /// <summary>
    /// Build the dialog with attached parameters.
    /// </summary>
    /// <returns>dialog object instance</returns>
    public DialogObject Build() {
        return BuildDialogViewPrivate().Item1;
    }
    
    /// <summary>
    /// Build the dialog control with attached parameters, also you will get an async accessor for access dialog state
    /// </summary>
    /// <returns>an dialog control and accessor to get dialog state asynchronously</returns>
    public Tuple<DialogObject, Func<CancellationToken, Task<object>>> BuildWithStateAccessor() {

        var (control, viewModel) = BuildDialogViewPrivate();
        
        return new Tuple<DialogObject, Func<CancellationToken, Task<object>>>(control, viewModel.State.DequeueAsync);
    }

    private Tuple<DialogObject, DialogControlViewModel> BuildDialogViewPrivate() {
        var obj = new DialogObject();
        
        var viewModel = new DialogControlViewModel {
            DialogHeader = new DialogHeaderViewModel {
                Header = _titleText,
                Icon = _titleView
            },
            Views = new AvaloniaList<object>(_elementSet),
            Answers = new AvaloniaList<DialogBuilderButtonViewModel>(_negativeButtons
                .Union(_positiveButtons)),
            AssistantButtons = new AvaloniaList<DialogBuilderButtonViewModel>(_neutralButtons)
        };
        
        var view = new DialogControlView {
            Content = viewModel
        };

        foreach (var button in viewModel.Answers.Union(viewModel.AssistantButtons)) {
            button._closeCommandSourceInternal = obj.CloseDialogInternal;
        }

        view.Styles.AddRange(_styleSet);

        obj.ViewModel = viewModel;
        obj.View = view;

        return new Tuple<DialogObject, DialogControlViewModel>(obj, viewModel);
    }

    private const string DialogViewCardName = "PART_DialogViewCard";

    /// <summary>
    /// Build the standalone dialog window with attached parameters.
    /// </summary>
    /// <returns>dialog window instance</returns>
    public Window BuildWindow() {
        return BuildDialogWindowPrivate().Item1;
    }

    private Tuple<Window, DialogControlViewModel> BuildDialogWindowPrivate() {
        var dialogView = BuildDialogViewPrivate();

        var view = new Card {
            Name = DialogViewCardName,
            Content = dialogView.Item1,
            CornerRadius = new CornerRadius(8)
        };

        var window = new Window {
            Content = view,
            SizeToContent = SizeToContent.WidthAndHeight,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            SystemDecorations = SystemDecorations.None,
            Styles = {
                new StyleInclude(new Uri("avares://Material.Avalonia.Dialogs/Styles/StyleInclude.axaml")) {
                    Source = new Uri("avares://Material.Avalonia.Dialogs/Styles/StyleInclude.axaml")
                }
            }
        };

        // Add event handler, remove them once window have been closed.
        // bind pointer pressed event to allow users drag dialog even without window topbar.
        view.AddHandler(InputElement.PointerPressedEvent, OnPointerPressedDialogViewPrivate);
        window.Closing += OnDialogWindowClosingPrivate;

        return new Tuple<Window, DialogControlViewModel>(window, dialogView.Item2);
    }


    /// <summary>
    /// Build a standalone window, show dialog with constantly receiving dialog state.
    /// </summary>
    /// <param name="owner">would be a parent window or any window that should cover before dialog get answered.</param>
    /// <param name="cancellationToken">cancellation token that used for break state channel loop.</param>
    /// <param name="modifier">modifier procedure before it shows up.</param>
    /// <returns>An enumerable state channel that you can receive new dialog state</returns>
    public async IAsyncEnumerable<object?> BuildAndShowDialogAsync(Window owner,
        [EnumeratorCancellation] CancellationToken cancellationToken = default, Action<Window>? modifier = null) {
        var windowInst = BuildDialogWindowPrivate();

        await foreach (var state in ShowDialogPrivateAsync(owner, cancellationToken, modifier, windowInst.Item1,
                           windowInst.Item2, ShowDialogWindowPrivateAsync!))
            yield return state;
    }

    /// <summary>
    /// Build a standalone window, show as window with constantly receiving dialog state.
    /// </summary>
    /// <param name="owner">would be a parent window or any window that should cover before dialog get answered.</param>
    /// <param name="cancellationToken">cancellation token that used for break state channel loop.</param>
    /// <param name="modifier">modifier procedure before it shows up.</param>
    /// <returns>An enumerable state channel that you can receive new dialog state</returns>
    public async IAsyncEnumerable<object?> BuildAndShowAsync(Window? owner = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default, Action<Window>? modifier = null) {
        var windowInst = BuildDialogWindowPrivate();

        await foreach (var state in ShowDialogPrivateAsync(owner, cancellationToken, modifier, windowInst.Item1,
                           windowInst.Item2, ShowWindowPrivateAsync))
            yield return state;
    }

    private static async IAsyncEnumerable<object?> ShowDialogPrivateAsync(Window? owner,
        [EnumeratorCancellation] CancellationToken cancellationToken,
        Action<Window>? modifier, Window window, DialogControlViewModel vm, Func<Window?, Window, Task> procedure) {
        modifier?.Invoke(window);

        var cancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        window.Closed += WindowOnClosed;

        void WindowOnClosed(object sender, EventArgs e) {
            cancellation.Cancel();

            window.Closed -= WindowOnClosed;
        }

        var task = procedure(owner, window);

        while (task.Status == TaskStatus.Running && !cancellation.IsCancellationRequested) {
            yield return await vm.State.DequeueAsync(cancellation.Token);
        }
    }

    private static Task ShowDialogWindowPrivateAsync(Window owner, Window window) {
        var task = window.ShowDialog(owner);
        return task;
    }

    private static Task ShowWindowPrivateAsync(Window? owner, Window window) {
        if (owner != null) {
            window.Show(owner);
        }
        else {
            window.Show();
        }

        return Task.CompletedTask;
    }

    private void OnDialogWindowClosingPrivate(object sender, WindowClosingEventArgs e) {
        if (sender is not Window window)
            return;

        var view = (Card)window.GetLogicalDescendants().First(a => {
            if (a is not Card c)
                return false;

            return c.Name == DialogViewCardName;
        });

        window.Closing -= OnDialogWindowClosingPrivate;
        view.RemoveHandler(InputElement.PointerPressedEvent, OnPointerPressedDialogViewPrivate);
    }

    private void OnPointerPressedDialogViewPrivate(object sender, PointerPressedEventArgs e) {
        if (sender is not Control control)
            return;

        var window = control.FindLogicalAncestorOfType<Window>();
        window?.BeginMoveDrag(e);
    }
}