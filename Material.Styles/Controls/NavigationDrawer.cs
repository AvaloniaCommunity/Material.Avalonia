using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.Metadata;

namespace Material.Styles.Controls
{
    [PseudoClasses(":open", ":closed", ":left", ":right")]
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
        
        
        public static readonly StyledProperty<object> RightDrawerContentProperty =
            AvaloniaProperty.Register<NavigationDrawer, object>(nameof(RightDrawerContent));
         
        public static readonly StyledProperty<IDataTemplate> RightDrawerContentTemplateProperty =
            AvaloniaProperty.Register<NavigationDrawer, IDataTemplate>(nameof(RightDrawerContentTemplate));

        public static readonly StyledProperty<bool> RightDrawerOpenedProperty =
            AvaloniaProperty.Register<NavigationDrawer, bool>(nameof(RightDrawerOpened));

        public static readonly StyledProperty<double> RightDrawerWidthProperty =
            AvaloniaProperty.Register<NavigationDrawer, double>(nameof(RightDrawerWidth));

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
        
        /// <summary>
        /// Gets or sets the content to display.
        /// </summary> 
        [DependsOn(nameof(RightDrawerContentTemplate))]
        public object RightDrawerContent
        {
            get => GetValue(RightDrawerContentProperty);
            set => SetValue(RightDrawerContentProperty, value);
        }

        /// <summary>
        /// Gets or sets the data template used to display the content of the control.
        /// </summary>
        public IDataTemplate RightDrawerContentTemplate
        {
            get => GetValue(RightDrawerContentTemplateProperty);
            set => SetValue(RightDrawerContentTemplateProperty, value);
        }

        public bool RightDrawerOpened
        {
            get => GetValue(RightDrawerOpenedProperty);
            set => SetValue(RightDrawerOpenedProperty, value);
        }

        public double RightDrawerWidth
        {
            get => GetValue(RightDrawerWidthProperty);
            set => SetValue(RightDrawerWidthProperty, value);
        }

        static NavigationDrawer()
        {
            LeftDrawerOpenedProperty.Changed.AddClassHandler<NavigationDrawer>(OnDrawerOpenedChanged);
            RightDrawerOpenedProperty.Changed.AddClassHandler<NavigationDrawer>(OnDrawerOpenedChanged);
        }

        private static void OnDrawerOpenedChanged(NavigationDrawer drawer, AvaloniaPropertyChangedEventArgs args)
        {
            drawer.UpdatePseudoClasses();
        }

        // ReSharper disable once InconsistentNaming
        private Border? PART_Scrim;

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            if (e.NameScope.Find("PART_Scrim") is Border border)
            {
                PART_Scrim = border;
                
                PART_Scrim.PointerPressed += PART_Scrim_Pressed;
            }

            base.OnApplyTemplate(e);
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            if(PART_Scrim != null)
                PART_Scrim.PointerPressed += PART_Scrim_Pressed;
            
            base.OnAttachedToVisualTree(e);
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            if(PART_Scrim != null)
                PART_Scrim.PointerPressed -= PART_Scrim_Pressed;
            
            base.OnDetachedFromVisualTree(e);
        }

        private void PART_Scrim_Pressed(object sender, RoutedEventArgs e)
        {
            LeftDrawerOpened = false;
            RightDrawerOpened = false;
        }

        private void UpdatePseudoClasses()
        {
            var open = LeftDrawerOpened || RightDrawerOpened;
            PseudoClasses.Add(open ? ":open" : ":closed");
            PseudoClasses.Remove(!open ? ":open" : ":closed");

            PseudoClasses.Set(":left", LeftDrawerOpened);
            PseudoClasses.Set(":right", RightDrawerOpened);
        }
    }
}
