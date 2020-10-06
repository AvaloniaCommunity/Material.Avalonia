using Avalonia;
using Avalonia.Animation;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using MaterialXamlToolKit.Avalonia.Additional;

namespace MaterialXamlToolKit.Avalonia
{
    public class MaterialToolKit : Styles
    {
        public MaterialToolKit() {
            AvaloniaXamlLoader.Load(this);
            Animation.RegisterAnimator<RelativePointAnimator>(property => typeof(RelativePoint).IsAssignableFrom(property.PropertyType));
        }
    }
}