using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Styling;
using System.Threading.Tasks;

namespace Material.Styles
{
    public class FloatingButton : Button
    {
        public static readonly StyledProperty<bool> IsExtendedProperty =
    AvaloniaProperty.Register<FloatingButton, bool>(nameof(IsExtended));

        public bool IsExtended
        {
            get { return GetValue(IsExtendedProperty); }
            set { SetValue(IsExtendedProperty, value); }
        }

        public FloatingButton() { 
        }  
    }
}