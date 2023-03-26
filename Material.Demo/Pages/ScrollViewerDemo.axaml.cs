using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Material.Demo.Pages {
    public partial class ScrollViewerDemo : UserControl {
        public ScrollViewerDemo() {
            this.InitializeComponent();
        }

        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
