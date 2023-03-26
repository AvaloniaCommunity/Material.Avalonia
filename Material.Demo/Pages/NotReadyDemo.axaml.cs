using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Material.Demo.Pages {
    public partial class NotReadyDemo : UserControl {
        public NotReadyDemo() {
            this.InitializeComponent();
        }

        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
