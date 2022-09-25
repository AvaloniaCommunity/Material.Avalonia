﻿using System;

namespace Material.Colors.ColorManipulation
{
    internal struct Lab
    {
        public double L { get; }
        public double A { get; }
        public double B { get; }

        public Lab(double l, double a, double b)
        {
            L = l;
            A = a;
            B = b;
        }
    }

    internal class LabConstants
    {
        public const double Kn = 18;

        public const double WhitePointX = 0.95047;
        public const double WhitePointY = 1;
        public const double WhitePointZ = 1.08883;
        public const double K = 24389 / 27.0;
        public const double E = 216 / 24389.0;

        public static double ECubedRoot = Math.Pow(E, 1.0 / 3);
    }
}