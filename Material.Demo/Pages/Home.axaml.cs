using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml; 

namespace Material.Demo.Pages
{
    public class Home : UserControl
    { 
        public Home()
        {
            this.InitializeComponent();
            DataContext = this;
             
            UseMaterialUIDarkTheme();
        }

        public void UseMaterialUIDarkTheme() => GlobalCommand.UseMaterialUIDarkTheme();

        public void UseMaterialUILightTheme() => GlobalCommand.UseMaterialUILightTheme();

        public void OpenProjectRepoLink() => GlobalCommand.OpenProjectRepoLink();

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
