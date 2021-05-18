using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;

namespace Material.Styles.Controls
{
    public class Rotator : ContentControl
    {
        public Rotator()
        {
            _stopwatch = new Stopwatch();
        }

        public bool Rotate
        {
            get => _rotate;
            set => SetAndRaise(RotateProperty, ref _rotate, value);
        }
        private bool _rotate;
        
        public static readonly DirectProperty<Rotator, bool> RotateProperty =
            AvaloniaProperty.RegisterDirect<Rotator, bool>(nameof(Rotate),
                delegate(Rotator rotator) { return rotator._rotate;},
                delegate(Rotator rotator, bool b) { rotator._rotate = b; rotator.SetRotate(b); });
        
        public void SetRotate(bool v)
        {
            if (!v)
            {
                rotateDegree = 0;
                _stopwatch.Stop();
            }
            else
            {
                _stopwatch.Start();
                Task.Run(delegate
                {
                    while (_rotate)
                    {
                        rotateDegree += speed * (_stopwatch.Elapsed - _prevTime).TotalMilliseconds;
                        while (rotateDegree > 360)
                            rotateDegree -= 360;
                        
                        _prevTime = _stopwatch.Elapsed;
                        Dispatcher.UIThread.InvokeAsync(
                            delegate
                            {
                                RenderTransform = new RotateTransform(rotateDegree);
                            });
                        
                        Thread.Sleep(10);
                    }
                });
            }
        }

        public double Speed
        {
            get => speed;
            set => SetAndRaise(SpeedProperty, ref speed, value);
        }
        
        public static readonly DirectProperty<Rotator, double> SpeedProperty =
            AvaloniaProperty.RegisterDirect<Rotator, double>(nameof(Speed),
                delegate(Rotator rotator) { return rotator.speed;},
                delegate(Rotator rotator, double v) { rotator.speed = v; });
        private double speed = 0.4;
        
        private double rotateDegree = 0;
        private Stopwatch _stopwatch;
        private TimeSpan _prevTime;
    }
}