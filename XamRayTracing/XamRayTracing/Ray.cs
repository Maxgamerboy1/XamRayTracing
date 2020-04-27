using System;
using System.Numerics;

namespace XamRayTracing
{
    public class Ray
    {
        private readonly int __InitialHeading;

        public Ray(Vector2 location, int initialHeading)
        {
            Location = location;
            Direction = VecUtils.RotateByDegrees(Location, initialHeading);
            __InitialHeading = initialHeading;
        }

        internal Vector2 Cast(Boundary wall, Vector2 playerHeading)
        {
            float _WallX_Start = wall.Origin.X;
            float _WallY_Start = wall.Origin.Y;
            float _WallX_End = wall.End.X;
            float _WallY_End = wall.End.Y;
            float _RayX_Start = Location.X;
            float _RayY_Start = Location.Y;


            Vector2 _Vec4 = Vector2.Add(Location, Direction);
            float _RayX_End = _Vec4.X;
            float _RayY_End = _Vec4.Y;

            float _Den = (_WallX_Start - _WallX_End) * (_RayY_Start - _RayY_End) - (_WallY_Start - _WallY_End) * (_RayX_Start - _RayX_End);
            if (_Den == 0F)
            {
                return Vector2.Zero;
            }

            float _T = ((_WallX_Start - _RayX_Start) * (_RayY_Start - _RayY_End) - (_WallY_Start - _RayY_Start) * (_RayX_Start - _RayX_End)) / _Den;
            float _U = -((_WallX_Start - _WallX_End) * (_WallY_Start - _RayY_Start) - (_WallY_Start - _WallY_End) * (_WallX_Start - _RayX_Start)) / _Den;
            if (_T > 0F && _T < 1F && _U > 0F)
            {
                return new Vector2(_WallX_Start + _T * (_WallX_End - _WallX_Start), _WallY_Start + _T * (_WallY_End - _WallY_Start));
            }
            else
            {
                return Vector2.Zero;
            }
        }

        internal void SetHeading(double heading_Deg)
        {
            Direction = VecUtils.RotateByDegrees(Location, __InitialHeading + heading_Deg);
        }

        public Vector2 Direction { get; set; }
        public Vector2 Location { get; set; }
    }
}
