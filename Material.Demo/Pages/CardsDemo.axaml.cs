using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Material.Demo.Pages {
    public partial class CardsDemo : UserControl {
        public CardsDemo() {
            this.InitializeComponent();
        }

        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
