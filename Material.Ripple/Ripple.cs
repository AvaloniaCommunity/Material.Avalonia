using System;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Layout;

namespace Material.Ripple {
    public class Ripple : Ellipse {
        public static Easing Easing {
            get => _easing;
            set {
                _easing = value;
                UpdateTransitions();
            }
        }

        public static TimeSpan Duration {
            get => _duration;
            set {
                _duration = value;
                UpdateTransitions();
            }
        }

        public static Transitions RippleTransitions;

        private static Easing _easing = new CircularEaseOut();
        private static TimeSpan _duration = new(0, 0, 0, 0, 500);



        private readonly double _maxDiam;

        private readonly double _endX;
        private readonly double _endY;

        static Ripple() {
            UpdateTransitions();
        }

        public Ripple(double outerWidth, double outerHeight, bool transitions = true) {
            Width = 0;
            Height = 0;

            _maxDiam = Math.Sqrt(Math.Pow(outerWidth, 2) + Math.Pow(outerHeight, 2));
            _endY = _maxDiam - outerHeight;
            _endX = _maxDiam - outerWidth;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            Opacity = 1;

            if (!transitions)
                return;

            Transitions = RippleTransitions;
        }

        public void SetupInitialValues(PointerPressedEventArgs e, Control parent) {
            var pointer = e.GetPosition(parent);
            Margin = new Thickness(pointer.X, pointer.Y, 0, 0);
        }

        public void RunFirstStep() {
            Width = _maxDiam;
            Height = _maxDiam;
            Margin = new Thickness(-_endX / 2, -_endY / 2, 0, 0);
        }

        public void RunSecondStep() {
            Opacity = 0;
        }

        private static void UpdateTransitions() {
            RippleTransitions = new Transitions {
                new ThicknessTransition {
                    Duration = Duration,
                    Easing = Easing,
                    Property = MarginProperty
                },
                new DoubleTransition {
                    Duration = Duration,
                    Easing = Easing,
                    Property = WidthProperty
                },
                new DoubleTransition {
                    Duration = Duration,
                    Easing = Easing,
                    Property = HeightProperty
                },
                new DoubleTransition {
                    Duration = Duration,
                    Easing = Easing,
                    Property = OpacityProperty
                }
            };
        }
    }
}
