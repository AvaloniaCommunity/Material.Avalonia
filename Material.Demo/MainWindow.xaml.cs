using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Material.Colors;
using Material.Styles.Assists;
using Material.Styles.Themes;
using Material.Styles.Themes.Base;
using PrimaryColor = Material.Colors.PrimaryColor;

namespace Material.Demo {
    public class MainWindow : Window {


        private PaletteHelper _paletteHelper;

        #region Control fields
        private ToggleButton NavDrawerSwitch;
        private ListBox DrawerList;
        private Carousel PageCarousel;
        #endregion

        public MainWindow() {
            InitializeComponent();
            this.AttachDevTools(KeyGesture.Parse("Shift+F12"));
        }

        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
            _paletteHelper = new PaletteHelper();

            UseMaterialUIDarkTheme(); 

            #region Control getter and event binding
            NavDrawerSwitch = this.Get<ToggleButton>(nameof(NavDrawerSwitch));

            DrawerList = this.Get<ListBox>(nameof(DrawerList));
            DrawerList.PointerReleased += DrawerSelectionChanged;

            PageCarousel = this.Get<Carousel>(nameof(PageCarousel));
            #endregion

            //this.FindControl<ToggleSwitch>("BaseThemeCheckBox").IsChecked = _paletteHelper.GetTheme().GetBaseTheme() == BaseThemeMode.Dark;
        }

        public void BaseThemeChanged(object sender, RoutedEventArgs args)
        {
            if (!(sender is ToggleSwitch checkBox)) return;
            ChangeDarkMode(checkBox.IsChecked!.Value);
        }
        public void ChangeDarkMode(bool isEnabled)
        {
            var theme = _paletteHelper.GetTheme();
            var baseThemeMode = isEnabled switch
            {
                true => BaseThemeMode.Dark,
                false => BaseThemeMode.Light
            };
            theme.SetBaseTheme(baseThemeMode.GetBaseTheme());
            _paletteHelper.SetTheme(theme);
        }

        public void UseMaterialUIDarkTheme()
        {
            var theme = _paletteHelper.GetTheme();
            theme.SetPrimaryColor(SwatchHelper.Lookup[MaterialColor.Blue200]);
            theme.SetSecondaryColor(SwatchHelper.Lookup[MaterialColor.Pink200]);
            theme.SetBaseTheme(BaseThemeMode.Dark.GetBaseTheme());
            _paletteHelper.SetTheme(theme);
        }
        public void BaseThemeColorChanged(object sender, RoutedEventArgs args)
        {
            if (!(sender is ToggleSwitch checkBox)) return;
            var theme = _paletteHelper.GetTheme();
            var color = checkBox.IsChecked!.Value switch {
                true  => PrimaryColor.Purple,
                false => PrimaryColor.Teal
            };
            theme.SetPrimaryColor(SwatchHelper.Lookup[(MaterialColor) color]);
            _paletteHelper.SetTheme(theme);
        }


        public void DrawerSelectionChanged(object sender, RoutedEventArgs args)
        {
            var listBox = sender as ListBox;
            try
            { 
                PageCarousel.SelectedIndex = listBox.SelectedIndex;
            }
            catch
            {
            }
            NavDrawerSwitch.IsChecked = false;
        }
    }
}