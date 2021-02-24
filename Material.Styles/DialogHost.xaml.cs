using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates; 

namespace Material.Styles {
    [Obsolete("Do not use DialogHost since it doesnt ready for use!")]
    public class DialogHost : ContentControl { 

        public static readonly StyledProperty<bool> InsideClippingProperty =
            AvaloniaProperty.Register<DialogHost, bool>(nameof(InsideClipping), true);

        /// <summary>
        /// Get or set the inside border clipping.
        /// </summary>
        public bool InsideClipping
        {
            get => GetValue(InsideClippingProperty);
            set => SetValue(InsideClippingProperty, value);
        }


        public static readonly StyledProperty<object> DialogContentProperty =
            AvaloniaProperty.Register<DialogHost, object>(nameof(DialogContent));
         
        public object DialogContent
        {
            get => GetValue(DialogContentProperty);
            set => SetValue(DialogContentProperty, value);
        }

        public static readonly StyledProperty<IDataTemplate> DialogTemplateContentProperty =
            AvaloniaProperty.Register<DialogHost, IDataTemplate>(nameof(DialogTemplateContent));
         
        public IDataTemplate DialogTemplateContent
        {
            get => GetValue(DialogTemplateContentProperty);
            set => SetValue(DialogTemplateContentProperty, value);
        }
    }
}