using SkiaSharp;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace XamRayTracing
{
    public class LightSource
    {
        public LightSource(float sceneWidth, float sceneHeight, double fov)
        {
            Position = new Vector2(sceneWidth / 2, sceneHeight / 2);
            SetRays(fov);
        }

        internal void Look(List<Boundary> walls, SKCanvas canvas)
        {
            foreach (Ray _Ray in Rays)
            {
                Vector2 _Closest = Vector2.Zero;
                float _Record = float.PositiveInfinity;
                foreach (Boundary _Wall in walls)
                {
                    _Ray.Position = Position;
                    Vector2 _Pt = _Ray.Cast(_Wall);
                    if (_Pt != Vector2.Zero)
                    {
                        float _D = Vector2.Distance(Position, _Pt);
                        if (_D < _Record)
                        {
                            _Record = _D;
                            _Closest = _Pt;
                        }
                    }
                }
                if (_Closest != Vector2.Zero)
                {
                    SKPoint _Start = new SKPoint(Position.X, Position.Y);
                    SKPoint _End = new SKPoint(_Closest.X, _Closest.Y);
                    canvas.DrawLine(_Start, _End, OverheadRoom.__WhitePaint);
                }
            }
        }

        internal void SetRays(double fov)
        {
            List<Ray> _Rays = new List<Ray>();
            for (int i = 0; i < fov; i++)
            {
                _Rays.Add(new Ray(Position, i));
            }

            Rays = _Rays;
        }

        public Vector2 Position { get; set; }
        public List<Ray> Rays { get; private set; }
    }
}
