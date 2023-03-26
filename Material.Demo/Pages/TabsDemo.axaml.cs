using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Material.Demo.Pages {
    public partial class TabsDemo : UserControl {
        public TabsDemo() {
            this.InitializeComponent();
        }

        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
