using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Material.Demo.ViewModels;
using Material.Dialog;

namespace Material.Demo.Pages
{
    public class DialogDemo : UserControl
    {


        public DialogDemo()
        {
            this.InitializeComponent();

            DataContext = new DialogDemoViewModel();
        } 

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
