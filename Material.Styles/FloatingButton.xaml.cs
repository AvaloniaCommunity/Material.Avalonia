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
        public FloatingButton() {
            this.AttachedToVisualTree += FloatingButton_Initialized; 
        } 
        private async void FloatingButton_Initialized(object sender, System.EventArgs e)
        { 
            ScaleTransform t = (ScaleTransform)(this.RenderTransform = new ScaleTransform(0, 0));

            t?.SetValue(ScaleTransform.ScaleXProperty, 1);
            t?.SetValue(ScaleTransform.ScaleYProperty, 1);
        }
    }
}