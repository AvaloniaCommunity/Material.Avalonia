using System;
using Avalonia.Animation.Easings;

namespace Material.Ripple {
    public static class Ripple {
        public static Easing Easing { get; set; } = new CircularEaseOut();
        public static TimeSpan Duration { get; set; } = new(0, 0, 0, 1, 200);
    }
}