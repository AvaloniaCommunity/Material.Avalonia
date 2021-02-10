using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Styling;
using System.Threading.Tasks;
using Avalonia.Layout;
using Material.Icons;
using Material.Icons.Avalonia;

namespace Material.Styles
{
    public class FloatingButton : Button
    {
        public static readonly StyledProperty<bool> IsExtendedProperty =
    AvaloniaProperty.Register<FloatingButton, bool>(nameof(IsExtended));

        public bool IsExtended
        {
            get => GetValue(IsExtendedProperty);
            set => SetValue(IsExtendedProperty, value);
        }

        public FloatingButton() { 
        }  
        
        /// <summary>
        /// Create FAB by easy way.
        /// </summary>
        /// <param name="iconKind"></param>
        /// <param name="text"></param>
        /// <returns>Instance of new FAB. <br/>
        /// MaterialIcon element name is PART_Icon and
        /// TextBlock name is PART_AdditionalText</returns>
        public static FloatingButton CreateFloatingButton(MaterialIconKind iconKind, string text = null)
        {
            FloatingButton button = new FloatingButton();
            button.Content = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Children =
                {
                    new MaterialIcon()
                    {
                        Name = "PART_Icon",
                        Kind = (MaterialIconKind)iconKind,
                        Width = 24, Height = 24
                    },
                    new TextBlock()
                    {
                        Name = "PART_AdditionalText",
                        Classes = Classes.Parse("Button"),
                        Margin = new Thickness(12,0,0,0),
                        VerticalAlignment = VerticalAlignment.Center,
                        Text = text,
                    }
                }
            };
            return button;
        }
    }
}