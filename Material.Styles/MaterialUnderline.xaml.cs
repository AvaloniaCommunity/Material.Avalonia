using System;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Threading;

namespace Material.Styles {
    public class MaterialUnderline : ContentControl {
        /// <summary>
        /// Defines the <see cref="BackgroundColor"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> BackgroundColorProperty =
            AvaloniaProperty.Register<MaterialUnderline, IBrush>(nameof(BackgroundColor));
        
        public IBrush BackgroundColor
        {
            get => GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }
        
        /// <summary>
        /// Defines the <see cref="ActiveColor"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> ActiveColorProperty =
            AvaloniaProperty.Register<MaterialUnderline, IBrush>(nameof(ActiveColor));
        
        public IBrush ActiveColor
        {
            get => GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }
        
        /// <summary>
        /// Defines the <see cref="IsActive"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsActiveProperty =
            AvaloniaProperty.Register<MaterialUnderline, bool>(nameof(IsActive));
        
        public bool IsActive
        {
            get => GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }
        
        /// <summary>
        /// Defines the <see cref="IsExpanded"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsExpandedProperty =
            AvaloniaProperty.Register<MaterialUnderline, bool>(nameof(IsExpanded));

        public bool IsExpanded
        {
            get => GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }
    }
}