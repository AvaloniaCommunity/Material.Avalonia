using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Material.Demo.Pages
{
    public class FieldsDemo : UserControl
    {
        public FieldsDemo()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
