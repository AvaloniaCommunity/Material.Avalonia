using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Material.Demo.ViewModels;

namespace Material.Demo.Pages
{
    public class FieldsDemo : UserControl
    {
        public FieldsDemo()
        {
            this.InitializeComponent();

            DataContext = new TextFieldsViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
