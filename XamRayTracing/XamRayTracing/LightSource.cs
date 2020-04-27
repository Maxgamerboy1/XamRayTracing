using SkiaSharp;
using System.Collections.Generic;
using System.Numerics;

namespace XamRayTracing
{
    public class LightSource
    {
        public LightSource(double fov)
        {
            Location = new Vector2(0,0);
            Heading = Vector2.Zero;
            SetRays(fov);
        }

        internal List<float> Look(List<Boundary> walls, SKCanvas canvas)
        {
            List<float> _Distances = new List<float>();
            foreach (Ray _Ray in Rays)
            {
                Vector2 _Closest = Vector2.Zero;
                float _Record = float.PositiveInfinity;
                foreach (Boundary _Wall in walls)
                {
                    _Ray.Location = Location;
                    Vector2 _HitPoint = _Ray.Cast(_Wall, Vector2.Add(Location, Heading));
                    if (_HitPoint != Vector2.Zero)
                    {
                        float _Distance = Vector2.Distance(Location, _HitPoint);
                        if (_Distance < _Record)
                        {
                            _Record = _Distance;
                            _Closest = _HitPoint;
                        }
                    }
                }
                if (_Closest != Vector2.Zero)
                {
                    SKPoint _Start = new SKPoint(Location.X, Location.Y);
                    SKPoint _End = new SKPoint(_Closest.X, _Closest.Y);
                    canvas.DrawLine(_Start, _End, OverheadRoom.__WhitePaint);
                }
                _Distances.Add(_Record);
            }

            return _Distances;
        }

        internal void SetRays(double fov)
        {
            List<Ray> _Rays = new List<Ray>();
            int index = 0;
            for (double step = 0; step < fov; step+= 1)
            {
                _Rays.Add(new Ray(Heading, index));
                index++;
            }

            Rays = _Rays;
        }

        internal void SetHeading(double heading)
        {
            Heading = VecUtils.RotateByDegrees(Location, heading);
            foreach (Ray ray in Rays)
            {
                ray.SetHeading(heading);
            }
        }

        public Vector2 Location { get; set; }
        public Vector2 Heading { get; private set; }
        public List<Ray> Rays { get; private set; }
    }
}
