using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Material.Styles.Controls
{
    public class MaterialUnderline : ContentControl
    {
        /// <summary>
        /// Defines the <see cref="IdleBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush?> IdleBrushProperty =
            AvaloniaProperty.Register<MaterialUnderline, IBrush?>(nameof(IdleBrush));

        public IBrush? IdleBrush
        {
            get => GetValue(IdleBrushProperty);
            set => SetValue(IdleBrushProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="ActiveBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush?> ActiveBrushProperty =
            AvaloniaProperty.Register<MaterialUnderline, IBrush?>(nameof(ActiveBrush));

        public IBrush? ActiveBrush
        {
            get => GetValue(ActiveBrushProperty);
            set => SetValue(ActiveBrushProperty, value);
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
        /// Defines the <see cref="IsHovered"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsHoveredProperty =
            AvaloniaProperty.Register<MaterialUnderline, bool>(nameof(IsHovered));

        public bool IsHovered
        {
            get => GetValue(IsHoveredProperty);
            set => SetValue(IsHoveredProperty, value);
        }
    }
}