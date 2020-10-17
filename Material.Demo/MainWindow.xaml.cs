using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Material.Colors;
using Material.Styles.Themes;
using Material.Styles.Themes.Base;
using PrimaryColor = Material.Colors.PrimaryColor;

namespace Material.Demo {
    public class MainWindow : Window {
        private PaletteHelper _paletteHelper;

        public MainWindow() {
            InitializeComponent();
            this.AttachDevTools(KeyGesture.Parse("Shift+F12"));
        }

        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
            _paletteHelper = new PaletteHelper();
            this.FindControl<ToggleSwitch>("BaseThemeCheckBox").IsChecked = _paletteHelper.GetTheme().GetBaseTheme() == BaseThemeMode.Dark;
        }

        private void BaseThemeChanged(object sender, RoutedEventArgs args) {
            if (sender is ToggleSwitch checkBox) {
                var theme = _paletteHelper.GetTheme();
                var baseThemeMode = checkBox.IsChecked!.Value switch {
                    true  => BaseThemeMode.Dark,
                    false => BaseThemeMode.Light
                };
                theme.SetBaseTheme(baseThemeMode.GetBaseTheme());
                _paletteHelper.SetTheme(theme);
            }
        }
        private void BaseThemeColorChanged(object sender, RoutedEventArgs args) {
            if (sender is ToggleSwitch checkBox) {
                var theme = _paletteHelper.GetTheme();
                var color = checkBox.IsChecked!.Value switch {
                    true  => PrimaryColor.Purple,
                    false => PrimaryColor.Teal
                };
                theme.SetPrimaryColor(SwatchHelper.Lookup[(MaterialColor) color]);
                _paletteHelper.SetTheme(theme);
            }
        }
    }
}