using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace Material.Styles.Builders
{
    public class FloatingButtonBuilder
    {
        private double spacing = 12.0;
        private Control? icon;
        private Control? text;

        /// <summary>
        /// Set spacing between icon and text views.
        /// </summary>
        public FloatingButtonBuilder SetSpacing(double spacing = 12.0)
        {
            this.spacing = spacing;
            return this;
        }

        public FloatingButtonBuilder SetIcon(Control view)
        {
            icon = view;
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
            text = view;
            return this;
        }

        /// <summary>
        /// Build and return instance floating action button.
        /// </summary>
        /// <returns>New instance FloatingButton.</returns>
        public FloatingButton Build()
        {
            Panel panel;
            
            if (icon != null)
            {
                panel = new Grid
                {
                    ColumnDefinitions = new ColumnDefinitions($"Auto, {spacing.ToString()}, *")
                };
                
                icon.SetValue(Grid.ColumnProperty, 0);
                panel.Children.Add(icon);
                
                text?.SetValue(Grid.ColumnProperty, 2);
            }
            else
            {
                panel = new Panel();
            }
            
            if(text != null)
                panel.Children.Add(text);
            
            var button = new FloatingButton
            {
                Content = panel
            };

            return button;
        }
    }
}