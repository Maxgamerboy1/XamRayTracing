using SkiaSharp;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace XamRayTracing
{
    public class LightSource
    {
        public LightSource(float sceneWidth, float sceneHeight, int numberOfRays)
        {
            Position = new Vector2(sceneWidth / 2, sceneHeight / 2);
            float _Angle = 0;
            Rays = new List<Ray>(Enumerable.Range(0, numberOfRays).Select(index =>
            {
                _Angle += 360f / numberOfRays;
                return new Ray(Position, _Angle);
            }));
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
                    canvas.DrawLine(_Start, _End, Room.__WhitePaint);
                }
            }
        }

        public Vector2 Position { get; set; }
        public List<Ray> Rays { get; set; }
    }
}
