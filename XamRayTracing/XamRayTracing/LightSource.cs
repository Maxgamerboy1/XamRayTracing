using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace XamRayTracing
{
    public class LightSource
    {
        private double __Heading;
        public LightSource(double fov)
        {
            Location = new Vector2(0,0);
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
                    Vector2 _HitPoint = _Ray.Cast(_Wall);
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
                _Rays.Add(new Ray(Location, index + __Heading, index));
                index++;
            }

            Rays = _Rays;
        }

        internal void SetDirection(double heading)
        {
            __Heading = heading;
            foreach (Ray ray in Rays)
            {
                ray.SetDirection(ray.Index + __Heading);
            }
        }

        public Vector2 Location { get; set; }
        public List<Ray> Rays { get; private set; }
    }
}
