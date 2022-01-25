using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Metadata;
using System;
using Avalonia.Controls.Primitives;

namespace Material.Styles
{
    // TODO: mobile variant
    [PseudoClasses(":open", ":closed", ":mobile")]
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

        static SideSheet()
        {
            SideSheetWidthProperty.Changed.AddClassHandler<SideSheet>((x, e) => x.SideSheetWidthChanged(e));
            SideSheetContentProperty.Changed.AddClassHandler<SideSheet>((x, e) => x.SideSheetContentChanged(e));
            SideSheetOpenedProperty.Changed.AddClassHandler<SideSheet>((x, e) => x.SideSheetOpenedChanged(e));
        }

        // Controls
        
        private Border? PART_Scrim;
        private Border? PART_SideSheet;
        
        // Fields
        private Action? m_LatelyEventCall;

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            
            PART_Scrim = e.NameScope.Find<Border>("PART_Scrim");
            PART_Scrim.PointerPressed += PART_Scrim_Pressed;
            
            PART_SideSheet = e.NameScope.Find<Border>("PART_SideSheet");
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);
            m_LatelyEventCall?.Invoke();
        }

        private void SideSheetWidthChanged(AvaloniaPropertyChangedEventArgs e) => PART_SideSheet?.SetValue(MarginProperty,
            SideSheetOpened ? new Thickness(0) : new Thickness(0, 0, -SideSheetWidth + Converters.MarginCreator.Offset, 0));

        private void SideSheetOpenedChanged(AvaloniaPropertyChangedEventArgs e)
        {
            // Try schedule a call after control attached to visual tree. 
            if((!PART_Scrim?.IsInitialized ?? false) && (!PART_SideSheet?.IsInitialized ?? false))
            {
                var param = new AvaloniaPropertyChangedEventArgs<bool>(this, 
                    SideSheetOpenedProperty, 
                    (bool)e.OldValue, 
                    (bool)e.NewValue, 
                    e.Priority);
                m_LatelyEventCall = () => SideSheetOpenedChanged(param);
                return;
            } 

            var value = (bool)e.NewValue; 
            SetPseudoClassesOpenState(value); 
        }

        private void SideSheetContentChanged(AvaloniaPropertyChangedEventArgs e)
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
            SideSheetOpened = false;
        }

        private void SetPseudoClassesOpenState(bool open)
        {
            PseudoClasses.Add(open ? ":open" : ":closed");
            PseudoClasses.Remove(!open ? ":open" : ":closed");
        }
    }
}
