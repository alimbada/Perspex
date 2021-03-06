﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace Perspex.Cairo
{
    using Cairo = global::Cairo;

    public static class CairoExtensions
    {
        public static Cairo.Color ToCairo(this Perspex.Media.Color color)
        {
            return new Cairo.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0, color.A / 255.0);
        }

        public static Cairo.Matrix ToCairo(this Matrix m)
        {
            return new Cairo.Matrix(m.M11, m.M12, m.M21, m.M22, m.M31, m.M32);
        }

        public static Cairo.PointD ToCairo(this Point p)
        {
            return new Cairo.PointD(p.X, p.Y);
        }

        public static Cairo.Rectangle ToCairo(this Rect rect)
        {
            return new Cairo.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static Rect ToPerspex(this Cairo.Rectangle rect)
        {
            return new Rect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static Rect ToPerspex(this Pango.Rectangle rect)
        {
            return new Rect(
                Pango.Units.ToDouble(rect.X),
                Pango.Units.ToDouble(rect.Y),
                Pango.Units.ToDouble(rect.Width),
                Pango.Units.ToDouble(rect.Height));
        }

        public static Pango.Weight ToCairo(this Perspex.Media.FontWeight weight)
        {
			return (Pango.Weight)weight;
        }

        public static Pango.Alignment ToCairo(this Perspex.Media.TextAlignment alignment)
        {
            if (alignment == Perspex.Media.TextAlignment.Left)
            {
                return Pango.Alignment.Left;
            }

            if (alignment == Perspex.Media.TextAlignment.Center)
            {
                return Pango.Alignment.Center;
            }

            return Pango.Alignment.Right;
        }
    }
}