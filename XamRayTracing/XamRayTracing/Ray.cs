using System;
using System.Numerics;

namespace XamRayTracing
{
    public class Ray
    {
        public Ray(Vector2 position, float angle)
        {
            Position = position;
            Direction = RotateBy(position, angle);
        }

        internal Vector2 Cast(Boundary wall)
        {
            float _X1 = wall.Origin.X;
            float _Y1 = wall.Origin.Y;
            float _X2 = wall.End.X;
            float _Y2 = wall.End.Y;
            float _X3 = Position.X;
            float _Y3 = Position.Y;

            Vector2 _Vec4 = Vector2.Add(Position, Direction);
            float _X4 = _Vec4.X;
            float _Y4 = _Vec4.Y;

            float _Den = (_X1 - _X2) * (_Y3 - _Y4) - (_Y1 - _Y2) * (_X3 - _X4);
            if (_Den == 0)
            {
                return Vector2.Zero;
            }

            float _T = ((_X1 - _X3) * (_Y3 - _Y4) - (_Y1 - _Y3) * (_X3 - _X4)) / _Den;
            float _U = -((_X1 - _X2) * (_Y1 - _Y3) - (_Y1 - _Y2) * (_X1 - _X3)) / _Den;
            if (_T > 0 && _T < 1 && _U > 0)
            {
                return new Vector2(_X1 + _T * (_X2 - _X1), _Y1 + _T * (_Y2 - _Y1));
            }
            else
            {
                return Vector2.Zero;
            }
        }

        private static Vector2 RotateBy(Vector2 v, float angle)
        {
            angle *= (float)Math.PI / 180F; // Convert to radians
            float _CosAngle = (float)Math.Cos(angle);
            float _SinAngle = (float)Math.Sin(angle);
            float _RotatedX = v.X * _CosAngle - v.Y * _SinAngle;
            float _RotatedY = (v.X * _SinAngle + v.Y * _CosAngle);

            return new Vector2(_RotatedX, _RotatedY);
        }

        public Vector2 Direction { get; set; }
        public Vector2 Position { get; set; }
    }
}
