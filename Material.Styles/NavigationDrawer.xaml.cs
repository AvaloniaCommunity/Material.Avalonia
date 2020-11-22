using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Metadata;

namespace Material.Styles
{
    public class NavigationDrawer : ContentControl
    { 
        public static readonly StyledProperty<object> LeftDrawerContentProperty =
            AvaloniaProperty.Register<NavigationDrawer, object>(nameof(LeftDrawerContent));
         
        public static readonly StyledProperty<IDataTemplate> LeftDrawerContentTemplateProperty =
            AvaloniaProperty.Register<NavigationDrawer, IDataTemplate>(nameof(LeftDrawerContentTemplate));

        public static readonly StyledProperty<bool> LeftDrawerOpenedProperty =
            AvaloniaProperty.Register<NavigationDrawer, bool>(nameof(LeftDrawerOpened));

        /// <summary>
        /// Gets or sets the content to display.
        /// </summary> 
        [DependsOn(nameof(LeftDrawerContentTemplate))]
        public object LeftDrawerContent
        {
            get { return GetValue(LeftDrawerContentProperty); }
            set { SetValue(LeftDrawerContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets the data template used to display the content of the control.
        /// </summary>
        public IDataTemplate LeftDrawerContentTemplate
        {
            get { return GetValue(LeftDrawerContentTemplateProperty); }
            set { SetValue(LeftDrawerContentTemplateProperty, value); }
        }

        public bool LeftDrawerOpened
        {
            get { return GetValue(LeftDrawerOpenedProperty); }
            set { SetValue(LeftDrawerOpenedProperty, value); }
        }

        static NavigationDrawer()
        {
            LeftDrawerContentProperty.Changed.AddClassHandler<NavigationDrawer>((x, e) => x.LeftDrawerContentChanged(e));
            LeftDrawerOpenedProperty.Changed.AddClassHandler<NavigationDrawer>((x, e) => x.LeftDrawerOpenedChanged(e));
        }

        private Border PART_Scrim;
        private Card PART_LeftDrawer;

        public NavigationDrawer()
        {
            this.TemplateApplied += (o, e) => {
                PART_Scrim = e.NameScope.Find("PART_Scrim") as Border;
                PART_Scrim.Transitions = new Transitions() 
                { 
                    new DoubleTransition() 
                    { 
                        Property = OpacityProperty, 
                        Duration = new System.TimeSpan(20000),  
                    } 
                };
                PART_Scrim.PointerPressed += PART_Scrim_Pressed;

                PART_LeftDrawer = e.NameScope.Find("PART_LeftDrawer") as Card;
                PART_LeftDrawer.Transitions = new Transitions()
                {
                    new ThicknessTransition()
                    {
                        Property = MarginProperty,
                        Duration = new System.TimeSpan(20000), 
                    }
                };
            };
        }

        private void LeftDrawerOpenedChanged(AvaloniaPropertyChangedEventArgs e)
        {
            var value = (bool)e.NewValue;
            PART_Scrim?.SetValue(OpacityProperty, value ? 0.32 : 0);
            PART_LeftDrawer?.SetValue(MarginProperty, value ? new Thickness(0) : new Thickness(-PART_LeftDrawer.Width - 8, 0, 0, 0)); 
        }

        private void LeftDrawerContentChanged(AvaloniaPropertyChangedEventArgs e)
        {
            if (e.OldValue is ILogical oldChild)
            {
                LogicalChildren.Remove(oldChild);
            }

            if (e.NewValue is ILogical newChild)
            {
                LogicalChildren.Add(newChild);
            }
        }
         
        private void PART_Scrim_Pressed(object sender, RoutedEventArgs e)
        {
            LeftDrawerOpened = false;
        }
    }
}
