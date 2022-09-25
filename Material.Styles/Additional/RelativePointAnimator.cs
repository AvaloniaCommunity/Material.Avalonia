using Avalonia;
using Avalonia.Animation.Animators;

namespace Material.Styles.Additional
{
    public class RelativePointAnimator : Animator<RelativePoint>
    {
        public override RelativePoint Interpolate(double progress, RelativePoint oldValue, RelativePoint newValue)
        {
            return new RelativePoint(
                (newValue.Point.X - oldValue.Point.X) * progress + oldValue.Point.X,
                (newValue.Point.Y - oldValue.Point.Y) * progress + oldValue.Point.Y,
                newValue.Unit);
        }
    }
}