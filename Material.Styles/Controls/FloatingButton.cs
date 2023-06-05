using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Controls {
    public class FloatingButton : Button {
        public static readonly StyledProperty<bool> IsExtendedProperty =
            AvaloniaProperty.Register<FloatingButton, bool>(nameof(IsExtended));

        public bool IsExtended {
            get => GetValue(IsExtendedProperty);
            set => SetValue(IsExtendedProperty, value);
        }

        /*[Obsolete("This function will be removed in next update. Use Material.Styles.Builders.FloatingButtonBuilder instead!")]
        public static FloatingButton CreateFloatingButton(MaterialIconKind iconKind, string text = null)
        {
            FloatingButton button = new FloatingButton();
            button.Content = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Children =
                {
                    new MaterialIcon
                    {
                        Name = "PART_Icon",
                        Kind = iconKind,
                        Width = 24, Height = 24
                    },
                    new TextBlock
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
        }*/
    }
}
