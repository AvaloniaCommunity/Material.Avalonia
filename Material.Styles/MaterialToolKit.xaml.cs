using Avalonia;
using Avalonia.Animation;
using Avalonia.Markup.Xaml;
using Material.Styles.Additional;

namespace Material.Styles {
    public class MaterialToolKit : Avalonia.Styling.Styles {
        static MaterialToolKit() {
            Animation.RegisterCustomAnimator<RelativePoint, RelativePointAnimator>();
        }

        public MaterialToolKit() {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
