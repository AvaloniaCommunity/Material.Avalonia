using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Material.Demo.Pages
{
    public class TogglesDemo : UserControl
    {
        public TogglesDemo()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
