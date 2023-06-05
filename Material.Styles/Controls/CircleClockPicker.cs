using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;

namespace Material.Styles.Controls
{
    public class CircleClockPicker : TemplatedControl
    {
        public static readonly DirectProperty<CircleClockPicker, int> ValueProperty =
            AvaloniaProperty.RegisterDirect<CircleClockPicker, int>(nameof(Value),
                o => o.Value, (o, v) => o.Value = v);

        public static readonly StyledProperty<int> MinimumProperty =
            AvaloniaProperty.Register<CircleClockPicker, int>(nameof(Minimum));

        public static readonly StyledProperty<int> MaximumProperty =
            AvaloniaProperty.Register<CircleClockPicker, int>(nameof(Maximum));

        public static readonly StyledProperty<int> StepFrequencyProperty =
            AvaloniaProperty.Register<CircleClockPicker, int>(nameof(StepFrequency));

        public static readonly StyledProperty<string?> FirstLabelOverrideProperty =
            AvaloniaProperty.Register<CircleClockPicker, string?>(nameof(FirstLabelOverride));

        public static readonly StyledProperty<double> RadiusMultiplierProperty =
            AvaloniaProperty.Register<CircleClockPicker, double>(nameof(RadiusMultiplier));

        public int Value
        {
            get => _value;
            set
            {
                SetAndRaise(ValueProperty, ref _value, value);
                UpdateVisual(value);
            }
        }

        public int Minimum
        {
            get => GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public int Maximum
        {
            get => GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public int StepFrequency
        {
            get => GetValue(StepFrequencyProperty);
            set => SetValue(StepFrequencyProperty, value);
        }

        public string? FirstLabelOverride
        {
            get => GetValue(FirstLabelOverrideProperty);
            set => SetValue(FirstLabelOverrideProperty, value);
        }

        public double RadiusMultiplier
        {
            get => GetValue(RadiusMultiplierProperty);
            set => SetValue(RadiusMultiplierProperty, value);
        }

        public event EventHandler? AfterDrag;

        static CircleClockPicker()
        {
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            _subscription?.Dispose();
            _subscription = null;

            var pointer = e.NameScope.Find<Control>("PART_Pointer");
            var canvas = e.NameScope.Find<Canvas>("PART_CellPanel");
            var pointerPin = e.NameScope.Find<Control>("PART_PointerPin");

            _pointer = pointer;
            _pointerPin = pointerPin;
            _cellPanel = canvas;

            _subscription = new CompositeDisposable
            {
                MinimumProperty.Changed.Subscribe(OnNext),
                MaximumProperty.Changed.Subscribe(OnNext),
                StepFrequencyProperty.Changed.Subscribe(OnNext),
                FirstLabelOverrideProperty.Changed.Subscribe(OnNext),
                RadiusMultiplierProperty.Changed.Subscribe(OnNext),
                BoundsProperty.Changed.Subscribe(OnCanvasResize)
            };

            UpdateCellPanel();
            AdjustPointer();
            UpdateVisual(_value);
        }

        private void OnCanvasResize(AvaloniaPropertyChangedEventArgs<Rect> obj)
        {
            if (!ReferenceEquals(obj.Sender, _cellPanel))
                return;

            UpdateCellPanel();
            AdjustPointer();
        }

        private void OnNext(EventArgs a)
        {
            UpdateCellPanel();
            AdjustPointer();
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);

            _isDragging = true;

            ProcessPointerEvent(e.GetPosition(this));
        }

        protected override void OnPointerMoved(PointerEventArgs e)
        {
            base.OnPointerMoved(e);

            if (!_isDragging)
                return;

            ProcessPointerEvent(e.GetPosition(this));
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            base.OnPointerReleased(e);

            _isDragging = false;
            AfterDrag?.Invoke(this, EventArgs.Empty);
        }

        private bool _isDragging;
        private Control? _pointer;
        private Control? _pointerPin;
        private Panel? _cellPanel;
        private readonly Dictionary<int, CircleClockPickerCell> _cachedAccessors = new();
        private IDisposable? _subscription;

        private int _value;

        private void ProcessPointerEvent(Point point)
        {
            var halfSize = (float) (Bounds.Width / 2);
            var rad = (float) Math.Atan2(point.Y - halfSize, point.X - halfSize);
            var degrees = rad * 180 / Math.PI + 90;

            if (degrees < 0)
                degrees += 360;

            if (degrees > 360)
                degrees -= 360;

            // degree to value
            var value = (int) Math.Round(degrees / 360 * (Maximum + 1 - Minimum) + Minimum);

            if (value == Maximum + 1)
                value = Minimum;

            if (!_cachedAccessors.TryGetValue(value, out var c))
                return;

            Value = c.Value;
        }

        private void UpdateVisual(int v)
        {
            if (!_cachedAccessors.TryGetValue(v, out var cell))
                return;

            foreach (var c in _cachedAccessors.Values)
            {
                c.IsSelected = false;

                if (!ReferenceEquals(c, cell))
                    continue;

                c.IsSelected = true;
            }

            if (_pointer == null)
                return;

            var degrees = (v - 90f) * (360f / (Maximum + 1 - Minimum));

            var transform = new RotateTransform(degrees);
            _pointer.RenderTransform = transform;
        }

        private void UpdateCellPanel()
        {
            if (_cellPanel == null)
                return;

            var step = StepFrequency;
            var min = Minimum;
            var max = Maximum;

            var radiusMultiplier = RadiusMultiplier;

            _cachedAccessors.Clear();
            _cellPanel.Children.Clear();

            void ArrangeCell(CircleClockPickerCell cell, double degree)
            {
                var canvasBounds = _cellPanel.Bounds;

                var w = canvasBounds.Width;
                var h = canvasBounds.Height;

                var hW = w / 2;
                var hH = h / 2;

                var rad = (float) ((degree - 90) * Math.PI / 180);

                var x = (float) (hW * radiusMultiplier * Math.Cos(rad)) + hW;
                var y = (float) (hH * radiusMultiplier * Math.Sin(rad)) + hH;

                cell.RenderTransform = new TranslateTransform(x, y);
            }

            float GetAngle(int value)
            {
                var degrees = (value - min) * (360f / (max + 1 - min));
                return degrees;
            }

            for (var i = min; i <= max; i++)
            {
                var cell = new CircleClockPickerCell
                {
                    Value = i
                };

                if (step > 0)
                {
                    if (i % step == 0)
                        cell.IsDot = false;
                }

                cell.Content = i.ToString();

                if (FirstLabelOverride != null && i == min)
                    cell.Content = FirstLabelOverride;

                _cellPanel.Children.Add(cell);

                ArrangeCell(cell, GetAngle(i));

                _cachedAccessors.Add(i, cell);
            }

            UpdateVisual(Value);
        }

        private void AdjustPointer()
        {
            if (_pointerPin == null)
                return;

            if (_cellPanel == null)
                return;

            var radius = _cellPanel.Bounds.Width / 2;
            _pointerPin.Height = radius * RadiusMultiplier;
        }
    }
}