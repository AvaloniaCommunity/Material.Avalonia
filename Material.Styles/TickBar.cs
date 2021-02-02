using Avalonia.Utilities;
using System;
using System.Collections.Generic;
using Avalonia;
using System.Text;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Layout;
using Avalonia.Collections;
using Material.Styles.Assists;

namespace Material.Styles
{

    /// <summary>
    /// An element that is used for drawing <see cref="Slider"/>'s Ticks.
    /// <br/>
    /// Those code taken from AvaloniaUI and modified.
    /// </summary>
    public class TickBar : Control
    {
        static TickBar()
        {
            AffectsRender<TickBar>(FillProperty,
                                   ReservedSpaceProperty,
                                   MaximumProperty,
                                   MinimumProperty,
                                   OrientationProperty, 
                                   TickFrequencyProperty,
                                   TicksProperty);
        }

        public TickBar() : base()
        {
        }

        /// <summary>
        /// Defines the <see cref="Fill"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> FillProperty =
            AvaloniaProperty.Register<TickBar, IBrush>(nameof(Fill));

        /// <summary>
        /// Brush used to fill the TickBar's Ticks.
        /// </summary>
        public IBrush Fill
        {
            get => GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="Minimum"/> property.
        /// </summary>
        public static readonly StyledProperty<double> MinimumProperty =
            AvaloniaProperty.Register<TickBar, double>(nameof(Minimum), 0d);

        /// <summary>
        /// Logical position where the Minimum Tick will be drawn
        /// </summary>
        public double Minimum
        {
            get => GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="Maximum"/> property.
        /// </summary>
        public static readonly StyledProperty<double> MaximumProperty =
            AvaloniaProperty.Register<TickBar, double>(nameof(Maximum), 0d);

        /// <summary>
        /// Logical position where the Maximum Tick will be drawn
        /// </summary>
        public double Maximum
        {
            get => GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="TickFrequency"/> property.
        /// </summary>
        public static readonly StyledProperty<double> TickFrequencyProperty =
            AvaloniaProperty.Register<TickBar, double>(nameof(TickFrequency), 0d);

        /// <summary>
        /// TickFrequency property defines how the tick will be drawn.
        /// </summary>
        public double TickFrequency
        {
            get => GetValue(TickFrequencyProperty);
            set => SetValue(TickFrequencyProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="Orientation"/> property.
        /// </summary>
        public static readonly StyledProperty<Orientation> OrientationProperty =
            AvaloniaProperty.Register<TickBar, Orientation>(nameof(Orientation));

        /// <summary>
        /// TickBar parent's orientation.
        /// </summary>
        public Orientation Orientation
        {
            get => GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="Ticks"/> property.
        /// </summary>
        public static readonly StyledProperty<AvaloniaList<double>> TicksProperty =
            AvaloniaProperty.Register<TickBar, AvaloniaList<double>>(nameof(Ticks));

        /// <summary>
        /// The Ticks property contains collection of value of type Double which
        /// are the logical positions use to draw the ticks.
        /// The property value is a <see cref="AvaloniaList{T}" />.
        /// </summary>
        public AvaloniaList<double> Ticks
        {
            get => GetValue(TicksProperty);
            set => SetValue(TicksProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="ReservedSpace"/> property.
        /// </summary>
        public static readonly StyledProperty<Rect> ReservedSpaceProperty =
            AvaloniaProperty.Register<TickBar, Rect>(nameof(ReservedSpace));

        /// <summary>
        /// TickBar will use ReservedSpaceProperty for left and right spacing (for horizontal orientation) or
        /// top and bottom spacing (for vertical orienation).
        /// The space on both sides of TickBar is half of specified ReservedSpace.
        /// This property has type of <see cref="Rect" />.
        /// </summary>
        public Rect ReservedSpace
        {
            get => GetValue(ReservedSpaceProperty);
            set => SetValue(ReservedSpaceProperty, value);
        }

        /// <summary>
        /// Draw ticks.
        /// Ticks can be draw in 8 different ways depends on Placement property and IsDirectionReversed property.
        ///
        /// This function also draw selection-tick(s) if IsSelectionRangeEnabled is 'true' and
        /// SelectionStart and SelectionEnd are valid.
        ///
        /// The primary ticks (for Mininum and Maximum value) height will be 100% of TickBar's render size (use Width or Height
        /// depends on Placement property).
        ///
        /// The secondary ticks (all other ticks, including selection-tics) height will be 75% of TickBar's render size.
        ///
        /// Brush that use to fill ticks is specified by Fill property.
        /// </summary>
        public override void Render(DrawingContext dc)
        {
            var size = new Size(Bounds.Width, Bounds.Height);
            var range = Maximum - Minimum; 
            var logicalToPhysical = 1.0;
            var startPoint = new Point();
            var endPoint = new Point();
            var rSpace = Orientation == Orientation.Horizontal ? ReservedSpace.Width : ReservedSpace.Height;
            var s = (double)GetValue(SliderAssist.SizeTickProperty);
            
            // Take Thumb size in to account
            double halfReservedSpace = rSpace * 0.5;

            var pen = new Pen(Fill, (double)GetValue(SliderAssist.ThicknessTickProperty));

            void DrawTick(double x, double y, Pen pen, double size,double scaleX = 0.5,double scaleY = 0.5)
            {
                double halfS = pen.Thickness * size * 0.5; 
                double dx = x * scaleX - halfS; 
                double dy = y * scaleY - halfS; 
                dc.DrawGeometry(pen.Brush, pen, new EllipseGeometry(
                    new Rect(dx, dy, size, size)));
            }
            
            // Reduce tick interval if it is more than would be visible on the screen
            double interval = TickFrequency;
            
            // This property is rarely set so let's try to avoid the GetValue
            // caching of the mutable default value
            var ticks = Ticks ?? null;
            
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    if (MathUtilities.GreaterThanOrClose(rSpace, size.Width))
                    {
                        return;
                    }
                    size = new Size(size.Width - rSpace, size.Height);
                    startPoint = new Point(halfReservedSpace, size.Height);
                    endPoint = new Point(halfReservedSpace + size.Width, size.Height);
                    logicalToPhysical = size.Width / range;
                    
                    if (interval > 0.0)
                    {
                        double minInterval = (Maximum - Minimum) / size.Width;
                        if (interval < minInterval)
                        {
                            interval = minInterval;
                        }
                    }

                    // Draw Min & Max tick
                    DrawTick(startPoint.X, startPoint.Y, pen, s, scaleX: 1);
                    DrawTick(endPoint.X, startPoint.Y, pen, s, scaleX: 1);

                    // Draw ticks using specified Ticks collection
                    if (ticks?.Count > 0)
                    {
                        for (int i = 0; i < ticks.Count; i++)
                        { 
                            double x = logicalToPhysical + startPoint.X;
                            DrawTick(x, startPoint.Y, pen, s, scaleX: 1);
                        }
                    }
                    // Draw ticks using specified TickFrequency
                    else if (interval > 0.0)
                    {
                        for (double i = interval; i < range; i += interval)
                        {
                            double x = i * logicalToPhysical + startPoint.X;
                            DrawTick(x, startPoint.Y, pen, s, scaleX: 1);
                        }
                    }
                    break;
                     
                case Orientation.Vertical:
                    if (MathUtilities.GreaterThanOrClose(rSpace, size.Height))
                    {
                        return;
                    }
                    size = new Size(size.Width, size.Height - rSpace);
                    startPoint = new Point(size.Width, size.Height + halfReservedSpace);
                    endPoint = new Point(size.Width, halfReservedSpace);
                    logicalToPhysical = size.Height / range * -1;
                    
                    if (interval > 0.0)
                    {
                        double minInterval = (Maximum - Minimum) / size.Height;
                        if (interval < minInterval)
                        {
                            interval = minInterval;
                        }
                    }

                    // Draw Min & Max tick
                    DrawTick(startPoint.X, startPoint.Y, pen, s, scaleY: 1);
                    DrawTick(startPoint.X, endPoint.Y, pen, s, scaleY: 1);

                    // Draw ticks using specified Ticks collection
                    if (ticks?.Count > 0)
                    {
                        for (int i = 0; i < ticks.Count; i++)
                        { 
                            double y = logicalToPhysical + startPoint.Y;
                            DrawTick(startPoint.X, y, pen, s, scaleY: 1);
                        }
                    }
                    // Draw ticks using specified TickFrequency
                    else if (interval > 0.0)
                    {
                        for (double i = interval; i < range; i += interval)
                        {
                            double y = i * logicalToPhysical + startPoint.Y;
                            DrawTick(startPoint.X, y, pen, s, scaleY: 1);
                        }
                    }
                    break; 
            }
        }
    }
}
