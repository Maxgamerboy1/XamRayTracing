using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace XamRayTracing
{
    internal class VecUtils
    {
        internal static Vector2 RotateByDegrees(Vector2 location, double deg)
        {
            double radAngle = deg * Math.PI / 180; // Convert to radians
            double _CosAngle = Math.Cos(radAngle);
            double _SinAngle = Math.Sin(radAngle);
            double _RotatedX = location.X * _CosAngle - location.Y * _SinAngle;
            double _RotatedY = location.X * _SinAngle + location.Y * _CosAngle;

            return new Vector2((float)_RotatedX, (float)_RotatedY);
        }
    }
}
