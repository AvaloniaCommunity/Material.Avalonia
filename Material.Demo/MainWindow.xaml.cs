using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Material.Styles.Themes;
using Material.Styles.Themes.Base;

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
            this.FindControl<CheckBox>("BaseThemeCheckBox").IsChecked = _paletteHelper.GetTheme().GetBaseTheme() == BaseThemeMode.Dark;
        }

        private void BaseThemeChanged(object sender, RoutedEventArgs args) {
            if (sender is CheckBox checkBox) {
                var theme = _paletteHelper.GetTheme();
                var baseThemeMode = checkBox.IsChecked!.Value switch {
                    true  => BaseThemeMode.Dark,
                    false => BaseThemeMode.Light
                };
                theme.SetBaseTheme(baseThemeMode.GetBaseTheme());
                _paletteHelper.SetTheme(theme);
            }
        }
    }
}