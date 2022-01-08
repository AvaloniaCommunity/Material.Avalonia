using System;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Rendering;

// ReSharper disable ConvertToLambdaExpression

namespace Material.Styles.Controls
{
    public class Rotator : ContentControl
    {
        private static IRenderLoop? _loopInstance;

        private static IRenderLoop LoopInstance => _loopInstance ??= new RenderLoop();

        // Minimum speed
        private double minimumSpeed = 0.0025;
        private bool _running;

        private double _speed = 0.4;

        private double _rotateDegree = 0;

        private IRenderLoop? _loop;
        private RenderLoopClock _loopTask;
        private TimeSpan _prev;

        public Rotator()
        {
            _loop = AvaloniaLocator.Current.GetService<IRenderLoop>() ?? LoopInstance;

            // Prepare render loop task for use.
            _loopTask = new RenderLoopClock();
            _loopTask.Subscribe(
                delegate(TimeSpan renderTime)
                {
                    var delta = renderTime - _prev;
                    _rotateDegree += _speed * delta.TotalMilliseconds;
                    _prev = renderTime;

                    while (_rotateDegree > 360)
                        _rotateDegree -= 360;

                    RenderTransform = new RotateTransform(_rotateDegree);
                });
        }

        public double Speed
        {
            get => _speed;
            set => SetAndRaise(SpeedProperty, ref _speed, value);
        }

        public static readonly DirectProperty<Rotator, double> SpeedProperty =
            AvaloniaProperty.RegisterDirect(nameof(Speed),
                delegate(Rotator rotator) { return rotator._speed; },
                delegate(Rotator rotator, double v)
                {
                    rotator._speed = v;
                    OnSpeedChanged(rotator, v);
                });

        // Loop dispatcher / simple loop controller
        private static void OnSpeedChanged(Rotator rotator, double d)
        {
            // We should stop rotator if speed is lower than minimum speed
            if (Math.Abs(d) < rotator.minimumSpeed)
            {
                // Stop render loop to avoid resources waste.
                if (!rotator._running)
                    return;

                // Reset statements
                rotator._running = false;
                rotator._rotateDegree = 0;

                // Detach loop task from RenderLoop
                rotator._loop.Remove(rotator._loopTask);

                return;
            }

            if (rotator._running)
                return;

            rotator._running = true;
            // Attach loop task to RenderLoop
            rotator._loop.Add(rotator._loopTask);
        }
    }
}