using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Material.Styles.Controls;

namespace Material.Styles.Builders
{
    public class FloatingButtonBuilder
    {
        private double _spacing = 12.0;
        private Control? _icon;
        private Control? _text;

        /// <summary>
        /// Set spacing between icon and text views.
        /// </summary>
        public FloatingButtonBuilder SetSpacing(double spacing = 12.0)
        {
            _spacing = spacing;
            return this;
        }

        public FloatingButtonBuilder SetIcon(Control view)
        {
            _icon = view;
            return this;
        }

        /// <summary>
        /// Create text view and attach to floating action button later.
        /// </summary>
        public FloatingButtonBuilder SetText(string text)
        {
            return SetText(new TextBlock
            {
                Name = "PART_AdditionalText",
                Classes = Classes.Parse("Button"),
                VerticalAlignment = VerticalAlignment.Center,
                Text = text
            });
        }

        public FloatingButtonBuilder SetText(TextBlock textBlock)
        {
            return SetText(view: textBlock);
        }

        public FloatingButtonBuilder SetText(Control view)
        {
            _text = view;
            return this;
        }

        /// <summary>
        /// Build and return instance floating action button.
        /// </summary>
        /// <returns>New instance FloatingButton.</returns>
        public FloatingButton Build()
        {
            Panel panel;

            if (_icon != null)
            {
                panel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                panel.Children.Add(_icon);

                var separator = new Separator
                {
                    Name = "PART_Spacing",
                    Width = _spacing,
                    Background = Brushes.Transparent,
                    Foreground = Brushes.Transparent
                };

                panel.Children.Add(separator);
            }
            else
            {
                panel = new Panel();
            }

            if (_text != null)
                panel.Children.Add(_text);

            var button = new FloatingButton
            {
                Content = panel
            };

            return button;
        }
    }
}