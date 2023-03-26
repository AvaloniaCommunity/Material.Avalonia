using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Material.Demo.Pages {
    public partial class SlidersDemo : UserControl {
        public SlidersDemo() {
            this.InitializeComponent();
        }

        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
