using System;
using System.Numerics;

namespace XamRayTracing
{
    public class Ray
    {
        public Ray(Vector2 location, double heading, int index)
        {
            Location = location;
            SetDirection(heading);
            Index = index;
        }

        internal Vector2 Cast(Boundary wall)
        {
            float _X1 = wall.Origin.X;
            float _Y1 = wall.Origin.Y;
            float _X2 = wall.End.X;
            float _Y2 = wall.End.Y;
            float _X3 = Location.X;
            float _Y3 = Location.Y;

            Vector2 _Vec4 = Vector2.Add(Location, Direction);
            float _X4 = _Vec4.X;
            float _Y4 = _Vec4.Y;

            float _Den = (_X1 - _X2) * (_Y3 - _Y4) - (_Y1 - _Y2) * (_X3 - _X4);
            if (_Den == 0F)
            {
                return Vector2.Zero;
            }

            float _T = ((_X1 - _X3) * (_Y3 - _Y4) - (_Y1 - _Y3) * (_X3 - _X4)) / _Den;
            float _U = -((_X1 - _X2) * (_Y1 - _Y3) - (_Y1 - _Y2) * (_X1 - _X3)) / _Den;
            if (_T > 0F && _T < 1F && _U > 0F)
            {
                return new Vector2(_X1 + _T * (_X2 - _X1), _Y1 + _T * (_Y2 - _Y1));
            }
            else
            {
                return Vector2.Zero;
            }
        }

        internal void SetDirection(double heading)
        {
            double radAngle = heading * Math.PI/ 180; // Convert to radians
            double _CosAngle = Math.Cos(radAngle);
            double _SinAngle = Math.Sin(radAngle);
            double _RotatedX = Location.X * _CosAngle - Location.Y * _SinAngle;
            double _RotatedY = Location.X * _SinAngle + Location.Y * _CosAngle;

            Direction = new Vector2((float)_RotatedX, (float)_RotatedY);
        }

        public Vector2 Direction { get; set; }
        public int Index { get; }
        public Vector2 Location { get; set; }
    }
}
