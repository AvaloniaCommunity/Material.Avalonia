using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Metadata;
using System;
using System.Threading.Tasks;

namespace Material.Styles
{
    [PseudoClasses(":open", ":closed")]
    public class NavigationDrawer : ContentControl
    { 
        public static readonly StyledProperty<object> LeftDrawerContentProperty =
            AvaloniaProperty.Register<NavigationDrawer, object>(nameof(LeftDrawerContent));
         
        public static readonly StyledProperty<IDataTemplate> LeftDrawerContentTemplateProperty =
            AvaloniaProperty.Register<NavigationDrawer, IDataTemplate>(nameof(LeftDrawerContentTemplate));

        public static readonly StyledProperty<bool> LeftDrawerOpenedProperty =
            AvaloniaProperty.Register<NavigationDrawer, bool>(nameof(LeftDrawerOpened));

        public static readonly StyledProperty<double> LeftDrawerWidthProperty =
            AvaloniaProperty.Register<NavigationDrawer, double>(nameof(LeftDrawerWidth));

        /// <summary>
        /// Gets or sets the content to display.
        /// </summary> 
        [DependsOn(nameof(LeftDrawerContentTemplate))]
        public object LeftDrawerContent
        {
            get => GetValue(LeftDrawerContentProperty);
            set => SetValue(LeftDrawerContentProperty, value);
        }

        /// <summary>
        /// Gets or sets the data template used to display the content of the control.
        /// </summary>
        public IDataTemplate LeftDrawerContentTemplate
        {
            get => GetValue(LeftDrawerContentTemplateProperty);
            set => SetValue(LeftDrawerContentTemplateProperty, value);
        }

        public bool LeftDrawerOpened
        {
            get => GetValue(LeftDrawerOpenedProperty);
            set => SetValue(LeftDrawerOpenedProperty, value);
        }

        public double LeftDrawerWidth
        {
            get => GetValue(LeftDrawerWidthProperty);
            set => SetValue(LeftDrawerWidthProperty, value);
        }

        static NavigationDrawer()
        {
            LeftDrawerWidthProperty.Changed.AddClassHandler<NavigationDrawer>((x, e) => x.LeftDrawerWidthChanged(e));
            LeftDrawerContentProperty.Changed.AddClassHandler<NavigationDrawer>((x, e) => x.LeftDrawerContentChanged(e));
            LeftDrawerOpenedProperty.Changed.AddClassHandler<NavigationDrawer>((x, e) => x.LeftDrawerOpenedChanged(e));
        }

        private Border PART_Scrim;
        private Border PART_LeftDrawerBorder;
        private Action m_LatelyEventCall;

        public NavigationDrawer()
        {
            this.TemplateApplied += (o, e) => {
                PART_Scrim = e.NameScope.Find("PART_Scrim") as Border;
                PART_Scrim.PointerPressed += PART_Scrim_Pressed;

                PART_LeftDrawerBorder = e.NameScope.Find("PART_LeftDrawerBorder") as Border;
            };
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);
            m_LatelyEventCall?.Invoke();
        }

        private void LeftDrawerWidthChanged(AvaloniaPropertyChangedEventArgs e) => PART_LeftDrawerBorder?.SetValue(MarginProperty,
            LeftDrawerOpened ? new Thickness(0) : new Thickness(-LeftDrawerWidth + Converters.MarginCreator.Offset, 0, 0, 0));

        private void LeftDrawerOpenedChanged(AvaloniaPropertyChangedEventArgs e)
        {
            // Try schedule a call after control attached to visual tree. 
            if(PART_Scrim is null && PART_LeftDrawerBorder is null
                && (!PART_Scrim?.IsInitialized ?? false) && (!PART_LeftDrawerBorder?.IsInitialized ?? false))
            {
                var param = new AvaloniaPropertyChangedEventArgs<bool>(this, 
                    LeftDrawerOpenedProperty, 
                    (bool)e.OldValue, 
                    (bool)e.NewValue, 
                    e.Priority);
                m_LatelyEventCall = () => LeftDrawerOpenedChanged(param);
                return;
            } 

            var value = (bool)e.NewValue; 
            SetPseudoClassesOpenState(value); 
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

        private void SetPseudoClassesOpenState(bool open)
        {
            PseudoClasses.Add(open ? ":open" : ":closed");
            PseudoClasses.Remove(!open ? ":open" : ":closed");
        }
    }
}
