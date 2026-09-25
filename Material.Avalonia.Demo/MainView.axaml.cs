using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Material.Avalonia.Demo.Pages;
using Material.Styles.Controls;
using Material.Styles.Models;
using Material.Styles.Themes;
using Material.Styles.Themes.Base;

namespace Material.Avalonia.Demo;

public partial class MainView : UserControl {
    private readonly Dictionary<int, Control> _pageCache = new();

    public MainView() {
        InitializeComponent();
        DrawerList.PointerReleased += DrawerSelectionChanged;
        DrawerList.KeyUp += DrawerList_KeyUp;
        PageContent.Content = GetPage(0);
    }

    public IReadOnlyList<DemoPageDefinition> PageDefinitions { get; } = new[] {
        new DemoPageDefinition("Home", () => new Home()),
        new DemoPageDefinition("Badges", () => new BadgesDemo()),
        new DemoPageDefinition("Buttons", () => new ButtonsDemo()),
        new DemoPageDefinition("Carousel", () => new CarouselDemo()),
        new DemoPageDefinition("Card", () => new CardsDemo()),
        new DemoPageDefinition("ColorZones", () => new ColorZonesDemo()),
        new DemoPageDefinition("Colors", () => new ColorsDemo()),
        new DemoPageDefinition("CommandBar", () => new CommandBarDemo()),
        new DemoPageDefinition("ComboBoxes", () => new ComboBoxesDemo()),
        new DemoPageDefinition("Date/Time pickers", () => new DateTimePickerDemo()),
        new DemoPageDefinition("Dialogs", () => new DialogDemo()),
        new DemoPageDefinition("Expanders", () => new ExpandersDemo()),
        new DemoPageDefinition("Fields", () => new FieldsDemo()),
        new DemoPageDefinition("Fields line up", () => new FieldsLineUpDemo()),
        new DemoPageDefinition("Lists", () => new ListsDemo()),
        new DemoPageDefinition("Material Icons", () => new IconsDemo(), ScrollBarVisibility.Disabled),
        new DemoPageDefinition("Pages", () => new PagesDemo()),
        new DemoPageDefinition("Progress indicators", () => new ProgressIndicatorDemo()),
        new DemoPageDefinition("ScrollViewer", () => new ScrollViewerDemo()),
        new DemoPageDefinition("SideSheet", () => new SideSheetDemo()),
        new DemoPageDefinition("Sliders", () => new SlidersDemo()),
        new DemoPageDefinition("Snackbar", () => new SnackbarDemo()),
        new DemoPageDefinition("TabControls", () => new TabsDemo()),
        new DemoPageDefinition("TableView", () => new TableViewDemo()),
        new DemoPageDefinition("Toggles", () => new TogglesDemo()),
        new DemoPageDefinition("TreeDataGrids", () => new TreeDataGridsDemo()),
        new DemoPageDefinition("TreeViews", () => new TreeViewsDemo()),
        new DemoPageDefinition("Typography", () => new TypographyDemo())
    };

    private void DrawerList_KeyUp(object? sender, KeyEventArgs e) {
        if (e.Key == Key.Space || e.Key == Key.Enter)
            DrawerSelectionChanged(sender, null);
    }

    public void DrawerSelectionChanged(object? sender, RoutedEventArgs? args) {
        if (sender is not ListBox listBox)
            return;

        if (!listBox.IsFocused && !listBox.IsKeyboardFocusWithin)
            return;
        try {
            if (listBox.SelectedIndex < 0 || listBox.SelectedIndex >= PageDefinitions.Count)
                return;

            PageContent.Content = GetPage(listBox.SelectedIndex);
            mainScroller.Offset = Vector.Zero;
            mainScroller.VerticalScrollBarVisibility =
                PageDefinitions[listBox.SelectedIndex].VerticalScrollBarVisibility;
        }
        catch {
            // ignored
        }

        LeftDrawer.OptionalCloseLeftDrawer();
    }

    private Control GetPage(int index) {
        if (!_pageCache.TryGetValue(index, out var page)) {
            page = PageDefinitions[index].Factory();
            _pageCache[index] = page;
        }

        return page;
    }


    private void TemplatedControl_OnTemplateApplied(object? sender, TemplateAppliedEventArgs e) {
        SnackbarHost.Post("Welcome to demo of Material.Avalonia!", null, DispatcherPriority.Normal);
    }

    /// <summary>
    /// This method is used for showcase of snackbar.
    /// </summary>
    private void HelloButtonMenuItem_OnClick(object? sender, RoutedEventArgs e) {
        // According to guidelines of Material design, 'endless' snackbar is not recommended.
        // They should dismiss after 4 - 10 seconds.
        // https://m2.material.io/components/snackbars#behavior
        var helloSnackBar = new SnackbarModel("Hello, user!", TimeSpan.FromSeconds(5));
        SnackbarHost.Post(helloSnackBar, null, DispatcherPriority.Normal);
    }

    /// <summary>
    /// This method is used for showcase of snackbar.
    /// </summary>
    private void GoodbyeButtonMenuItem_OnClick(object? sender, RoutedEventArgs e) {
        SnackbarHost.Post("See ya next time, user!", null, DispatcherPriority.Normal);
    }

    /// <summary>
    /// This method is used for showcase of snackbar.
    /// </summary>
    private void ConnectToNetworkMenuItem_OnClick(object? sender, RoutedEventArgs e) {
        void Retry() {
            SnackbarHost.Post(
                new SnackbarModel("Connected to network.", TimeSpan.FromSeconds(5)),
                null, DispatcherPriority.Normal);
        }

        SnackbarHost.Post(
            new SnackbarModel("Unable to connect network. Please check everything is fine.",
                TimeSpan.FromSeconds(10),
                new SnackbarButtonModel {
                    Text = "Retry",
                    Action = Retry
                }), null, DispatcherPriority.Normal);
    }

    private void MaterialIcon_OnPointerPressed(object? sender, PointerPressedEventArgs e) {
        var materialTheme = Application.Current!.LocateMaterialTheme<MaterialTheme>();
        materialTheme.BaseTheme =
            materialTheme.BaseTheme == BaseThemeMode.Light ? BaseThemeMode.Dark : BaseThemeMode.Light;
    }
}

public sealed record DemoPageDefinition(
    string Title,
    Func<Control> Factory,
    ScrollBarVisibility VerticalScrollBarVisibility = ScrollBarVisibility.Auto);