using Avalonia;
using Avalonia.Animation;
using Avalonia.Markup.Xaml;
using Material.Styles.Additional;

namespace Material.Styles
{
    public class MaterialToolKit : Avalonia.Styling.Styles
    {
        public MaterialToolKit()
        {
            AvaloniaXamlLoader.Load(this);
            Animation.RegisterAnimator<RelativePointAnimator>(property =>
                typeof(RelativePoint).IsAssignableFrom(property.PropertyType));
        }
    }
}