using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.Metadata;
using Avalonia.Controls.Primitives;
using Material.Styles.Enums;

namespace Material.Styles
{
    // TODO: mobile variant
    [PseudoClasses(":open", ":closed", ":left", ":right", ":mobile")]
    public class SideSheet : ContentControl
    { 
        // Avalonia properties
        
        public static readonly StyledProperty<object> SideSheetContentProperty =
            AvaloniaProperty.Register<SideSheet, object>(nameof(SideSheetContent));
         
        public static readonly StyledProperty<IDataTemplate> SideSheetContentTemplateProperty =
            AvaloniaProperty.Register<SideSheet, IDataTemplate>(nameof(SideSheetContentTemplate));

        public static readonly StyledProperty<bool> SideSheetOpenedProperty =
            AvaloniaProperty.Register<SideSheet, bool>(nameof(SideSheetOpened));

        public static readonly StyledProperty<double> SideSheetWidthProperty =
            AvaloniaProperty.Register<SideSheet, double>(nameof(SideSheetWidth));

        public static readonly StyledProperty<HorizontalDirection> SideSheetDirectionProperty =
            AvaloniaProperty.Register<SideSheet, HorizontalDirection>(nameof(SideSheetDirection));

        // CLR properties
        
        /// <summary>
        /// Gets or sets the content to display.
        /// </summary> 
        [DependsOn(nameof(SideSheetContentTemplate))]
        public object SideSheetContent
        {
            get => GetValue(SideSheetContentProperty);
            set => SetValue(SideSheetContentProperty, value);
        }

        /// <summary>
        /// Gets or sets the data template used to display the content of the control.
        /// </summary>
        public IDataTemplate SideSheetContentTemplate
        {
            get => GetValue(SideSheetContentTemplateProperty);
            set => SetValue(SideSheetContentTemplateProperty, value);
        }

        public bool SideSheetOpened
        {
            get => GetValue(SideSheetOpenedProperty);
            set => SetValue(SideSheetOpenedProperty, value);
        }

        public double SideSheetWidth
        {
            get => GetValue(SideSheetWidthProperty);
            set => SetValue(SideSheetWidthProperty, value);
        }
        
        public HorizontalDirection SideSheetDirection
        {
            get => GetValue(SideSheetDirectionProperty);
            set => SetValue(SideSheetDirectionProperty, value);
        }

        static SideSheet()
        {
            SideSheetOpenedProperty.Changed.AddClassHandler<SideSheet>(OnSideSheetStateChanged);
            SideSheetDirectionProperty.Changed.AddClassHandler<SideSheet>(OnSideSheetStateChanged);
        }

        private static void OnSideSheetStateChanged(SideSheet control, AvaloniaPropertyChangedEventArgs args)
        {
            control.UpdatePseudoClasses();
        }

        public SideSheet()
        {
            UpdatePseudoClasses();
        }

        // Controls
        
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
            SideSheetOpened = false;
        }

        private void UpdatePseudoClasses()
        {
            var open = SideSheetOpened;
            PseudoClasses.Add(open ? ":open" : ":closed");
            PseudoClasses.Remove(!open ? ":open" : ":closed");

            var direction = SideSheetDirection;
            PseudoClasses.Set(":left", direction == HorizontalDirection.Left);
            PseudoClasses.Set(":right", direction == HorizontalDirection.Right);
        }
    }
}
