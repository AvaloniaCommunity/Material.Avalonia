using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Utilities;

// ReSharper disable MemberCanBePrivate.Global

namespace Material.Styles.Controls
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
            AffectsRender<TickBar>(IsDirectionReversedProperty,
                InactiveBrushProperty,
                ActiveBrushProperty,
                ReservedSpaceProperty,
                MaximumProperty,
                MinimumProperty,
                ValueProperty,
                OrientationProperty,
                ThicknessTickProperty,
                TickFrequencyProperty,
                TicksProperty);
        }

        public static readonly StyledProperty<bool> IsDirectionReversedProperty =
            AvaloniaProperty.Register<TickBar, bool>(nameof(IsDirectionReversed));

        public bool IsDirectionReversed
        {
            get => GetValue(IsDirectionReversedProperty);
            set => SetValue(IsDirectionReversedProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="InactiveBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> InactiveBrushProperty =
            AvaloniaProperty.Register<TickBar, IBrush>(nameof(InactiveBrush));

        /// <summary>
        /// Brush used to draw the TickBar's Ticks on inactive track.
        /// </summary>
        public IBrush InactiveBrush
        {
            get => GetValue(InactiveBrushProperty);
            set => SetValue(InactiveBrushProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="ActiveBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> ActiveBrushProperty =
            AvaloniaProperty.Register<TickBar, IBrush>(nameof(ActiveBrush));

        /// <summary>
        /// Brush used to draw the TickBar's Ticks on active track.
        /// </summary>
        public IBrush ActiveBrush
        {
            get => GetValue(ActiveBrushProperty);
            set => SetValue(ActiveBrushProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="Minimum"/> property.
        /// </summary>
        public static readonly StyledProperty<double> MinimumProperty =
            AvaloniaProperty.Register<TickBar, double>(nameof(Minimum));

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
            AvaloniaProperty.Register<TickBar, double>(nameof(Maximum));

        /// <summary>
        /// Logical position where the Maximum Tick will be drawn
        /// </summary>
        public double Maximum
        {
            get => GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="Value"/> property.
        /// </summary>
        public static readonly StyledProperty<double> ValueProperty =
            AvaloniaProperty.Register<TickBar, double>(nameof(Value));

        /// <summary>
        /// Logical position where the Maximum Tick will be drawn
        /// </summary>
        public double Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="ThicknessTick"/> property.
        /// </summary>
        public static readonly StyledProperty<double> ThicknessTickProperty =
            AvaloniaProperty.Register<TickBar, double>(nameof(ThicknessTick));

        public double ThicknessTick
        {
            get => GetValue(ThicknessTickProperty);
            set => SetValue(ThicknessTickProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="TickFrequency"/> property.
        /// </summary>
        public static readonly StyledProperty<double> TickFrequencyProperty =
            AvaloniaProperty.Register<TickBar, double>(nameof(TickFrequency));

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
        /// TickBar will use ReservedSpaceProperty for horizontal orientation spacing or
        /// vertical orientation spacing.
        /// The space on both sides of TickBar is half of specified ReservedSpace.
        /// This property has type of <see cref="Rect" />.
        /// </summary>
        public Rect ReservedSpace
        {
            get => GetValue(ReservedSpaceProperty);
            set => SetValue(ReservedSpaceProperty, value);
        }

        public override void Render(DrawingContext dc)
        {
            var size = new Size(Bounds.Width, Bounds.Height);
            var range = Maximum - Minimum;
            double logicalToPhysical;
            Point startPoint, endPoint;
            var rSpace = Orientation == Orientation.Horizontal ? ReservedSpace.Width : ReservedSpace.Height;
            var left = Value;
            var reverse = IsDirectionReversed;

            // Take Thumb size in to account
            var halfReservedSpace = rSpace * 0.5;

            var thicknessTick = ThicknessTick;

            switch (Orientation)
            {
                case Orientation.Horizontal:
                    if (MathUtilities.GreaterThanOrClose(rSpace, size.Width))
                        return;

                    var hHeight = size.Height / 2;
                    size = new Size(size.Width - rSpace, size.Height);
                    startPoint = new Point(halfReservedSpace, hHeight);
                    endPoint = new Point(halfReservedSpace + size.Width, hHeight);
                    logicalToPhysical = size.Width / range;
                    break;

                case Orientation.Vertical:
                    if (MathUtilities.GreaterThanOrClose(rSpace, size.Height))
                        return;

                    var hWidth = size.Width / 2;
                    size = new Size(size.Width, size.Height - rSpace);
                    startPoint = new Point(hWidth, size.Height + halfReservedSpace);
                    endPoint = new Point(hWidth, halfReservedSpace);
                    logicalToPhysical = size.Height / range * -1;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Invert direction of the ticks
            if (reverse)
            {
                logicalToPhysical *= -1;

                // swap startPoint & endPoint
                (startPoint, endPoint) = (endPoint, startPoint);
            }

            var inactiveBrush = InactiveBrush.ToImmutable();
            var activeBrush = ActiveBrush.ToImmutable();

            void DrawTick(IBrush brush, Point centerPoint, double radius)
            {
                dc.DrawEllipse(brush, null, centerPoint, radius, radius);
            }

            IBrush PickBrushByRange(double c)
            {
                if (c < left)
                    return reverse ? inactiveBrush : activeBrush;

                return reverse ? activeBrush : inactiveBrush;
            }

            switch (Orientation)
            {
                // Horizontal
                case Orientation.Horizontal:
                {
                    // Reduce tick interval if it is more than would be visible on the screen
                    var interval = TickFrequency;
                    if (interval > 0.0)
                    {
                        var minInterval = (Maximum - Minimum) / size.Width;
                        if (interval < minInterval)
                            interval = minInterval;
                    }

                    // Draw Min & Max tick
                    DrawTick(PickBrushByRange(0), startPoint, thicknessTick);
                    DrawTick(PickBrushByRange(range), endPoint, thicknessTick);

                    // This property is rarely set so let's try to avoid the GetValue
                    // caching of the mutable default value
                    // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
                    var ticks = Ticks ?? null;

                    // Draw ticks using specified Ticks collection
                    if (ticks?.Count > 0)
                    {
                        for (var i = 0; i < ticks.Count; i++)
                        {
                            var tick = ticks[i];
                            if (MathUtilities.LessThanOrClose(tick, Minimum) ||
                                MathUtilities.GreaterThanOrClose(tick, Maximum))
                                continue;

                            var adjustedTick = tick - Minimum;

                            var x = adjustedTick * logicalToPhysical + startPoint.X;
                            var point = new Point(x, startPoint.Y);

                            DrawTick(PickBrushByRange(tick), point, thicknessTick);
                        }
                    }
                    // Draw ticks using specified TickFrequency
                    else if (interval > 0.0)
                    {
                        for (var i = interval; i < range; i += interval)
                        {
                            var x = i * logicalToPhysical + startPoint.X;
                            var point = new Point(x, startPoint.Y);

                            DrawTick(PickBrushByRange(i), point, thicknessTick);
                        }
                    }
                }
                    break;

                // Vertical
                case Orientation.Vertical:
                {
                    // Reduce tick interval if it is more than would be visible on the screen
                    var interval = TickFrequency;
                    if (interval > 0.0)
                    {
                        var minInterval = (Maximum - Minimum) / size.Height;
                        if (interval < minInterval)
                            interval = minInterval;
                    }

                    // Draw Min & Max tick
                    DrawTick(PickBrushByRange(0), startPoint, thicknessTick);
                    DrawTick(PickBrushByRange(range), endPoint, thicknessTick);

                    // This property is rarely set so let's try to avoid the GetValue
                    // caching of the mutable default value
                    // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
                    var ticks = Ticks ?? null;

                    // Draw ticks using specified Ticks collection
                    if (ticks?.Count > 0)
                    {
                        for (var i = 0; i < ticks.Count; i++)
                        {
                            var tick = ticks[i];
                            if (MathUtilities.LessThanOrClose(tick, Minimum) ||
                                MathUtilities.GreaterThanOrClose(tick, Maximum))
                                continue;

                            var adjustedTick = ticks[i] - Minimum;

                            var y = adjustedTick * logicalToPhysical + startPoint.Y;
                            var point = new Point(startPoint.X, y);

                            DrawTick(PickBrushByRange(tick), point, thicknessTick);
                        }
                    }
                    // Draw ticks using specified TickFrequency
                    else if (interval > 0.0)
                    {
                        for (var i = interval; i < range; i += interval)
                        {
                            var y = i * logicalToPhysical + startPoint.Y;
                            var point = new Point(startPoint.X, y);

                            DrawTick(PickBrushByRange(i), point, thicknessTick);
                        }
                    }
                }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}