using Avalonia;
using Avalonia.Markup.Xaml;

namespace AvaloniaRipple
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}