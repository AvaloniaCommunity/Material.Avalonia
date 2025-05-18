using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml.Styling;
using Material.Dialog.ViewModels;
using Material.Dialog.Views;
using Material.Styles.Assists;
using Material.Styles.Controls;

namespace Material.Dialog;

public partial class DialogObject
{
    internal DialogControlViewModel ViewModel { get; set; }
    
    internal DialogControlView View { get; set; }

    private Action<object>? _currentDialogCloseDelegate;
    
    internal void CloseDialogInternal(object result) {
        
        var action = _currentDialogCloseDelegate;
        
        if(action == null) 
            throw new InvalidOperationException("Wait wtf? the dialog close delegate is gone");
        
        action.Invoke(result);
    }
    
    /// <summary>
    /// Use custom dialog API or system to show and close dialog.
    /// </summary>
    /// <param name="showDialogDelegate">delegate to used for create and show dialog by your own way.</param>
    /// <param name="closeDialogDelegate">delegate that used for close and providing return value to your own dialog system.</param>
    /// <returns>final dialog result</returns>
    /// <exception cref="ArgumentNullException">one or more arguments is null or invalid.</exception>
    public async Task<object?> ShowCustomAsync(
        Func<DialogControlView, Task<object?>> showDialogDelegate, 
        Action<object> closeDialogDelegate) {

        if (showDialogDelegate == null)
            throw new ArgumentNullException(nameof(showDialogDelegate));
        
        ApplyDialogCloseDelegatePrivate(closeDialogDelegate);
        
        return await showDialogDelegate.Invoke(View);
    }

    /// <summary>
    /// Show standalone dialog and return final dialog result by dialog button.
    /// </summary>
    /// <param name="owner">owner window that used to cover up before dialog closed.</param>
    /// <returns>final dialog result</returns>
    public async Task<object?> ShowDialogAsync(Window owner) {
        var window = ShowDialogPreparePrivate();

        ApplyDialogCloseDelegatePrivate(a => window.Close(a));

        return await window.ShowDialog<object>(owner);
    }
    
    private const string DialogViewCardName = "PART_DialogViewCard";
    
    private Window ShowDialogPreparePrivate() {
        var view = new Card {
            Name = DialogViewCardName,
            Content = View,
            CornerRadius = new CornerRadius(8),
            MinWidth = 24,
            MinHeight = 24,
        };

        view.SetValue(ShadowAssist.ShadowDepthProperty, ShadowDepth.CenterDepth3, BindingPriority.Style);
        view.SetValue(TemplatedControl.PaddingProperty, new Thickness(), BindingPriority.Style);

        var window = new Window {
            Content = view,
            CanResize = true,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            SystemDecorations = SystemDecorations.None,
            MinWidth = 64,
            MinHeight = 64,
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

        return window;
    }

    private void ApplyDialogCloseDelegatePrivate(Action<object> action) {
        if (_currentDialogCloseDelegate != null)
            throw new InvalidOperationException("Current dialog is active and busy. Close it or build another instance to go.");

        _currentDialogCloseDelegate = action ?? throw new ArgumentNullException(nameof(action));
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
        
        var p = e.GetPosition(control);
        var resize = IsInResizeZone(control, p);

        if (resize.HasValue) {
            window?.BeginResizeDrag(resize.Value, e);
            return;
        }
        
        window?.BeginMoveDrag(e);
    }
    
    private WindowEdge? IsInResizeZone(Control c, Point p)
    {
        var b = c.Bounds;
        const double s = 16;
        var rT = b.Width - s;

        if (p.X > rT && p.Y > s)
            return WindowEdge.East;
        
        else if (p.X > rT && p.Y <= s)
            return WindowEdge.NorthEast;
        
        else if (p.X > s && p.Y <= s)
            return WindowEdge.North;
        
        else if (p.X <= s && p.Y <= s)
            return WindowEdge.NorthWest;

        return null;
    }
}