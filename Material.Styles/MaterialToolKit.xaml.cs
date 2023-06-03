using Avalonia.Markup.Xaml;

namespace Material.Styles {
    public class MaterialToolKit : Avalonia.Styling.Styles {
        public MaterialToolKit() {
            AvaloniaXamlLoader.Load(this);

            // TODO: Next Avalonia release - register animator missing https://github.com/AvaloniaUI/Avalonia/issues/11594
            // Animation.RegisterAnimator<RelativePointAnimator>(property =>
            //     typeof(RelativePoint).IsAssignableFrom(property.PropertyType));
        }
    }
}
