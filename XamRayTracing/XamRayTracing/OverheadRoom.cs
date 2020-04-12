using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Xamarin.Forms;

namespace XamRayTracing
{
    public class OverheadRoom : SKCanvasView
    {
        private readonly Random __Random = new Random();
        internal static SKPaint __RedPaint = new SKPaint
        {
            Color = Color.Red.ToSKColor(),
            IsAntialias = true,
            StrokeWidth = 2
        };
        private Vector2 __StartingPoint;
        private List<Boundary> __Walls;
        internal static SKPaint __WhitePaint = new SKPaint
        {
            Color = Color.White.ToSKColor(),
            IsAntialias = true,
            StrokeWidth = 2
        };

        public LightSource LightSource { get; set; }

        public OverheadRoom()
        {
            LightSource = new LightSource(CanvasSize.Width, CanvasSize.Height, 60);
        }

        internal void BeginTranslation()
        {
            __StartingPoint = LightSource.Position;
        }

        internal void ChangeBoundaries()
        {
            GenerateBoundaries();
            InvalidateSurface();
        }

        internal void EndTranslation()
        {
            __StartingPoint = Vector2.Zero;
        }

        private void GenerateBoundaries()
        {
            __Walls = new List<Boundary>(Enumerable.Range(0, 8).Select(index => new Boundary(__Random.Next((int)CanvasSize.Width), __Random.Next((int)CanvasSize.Height),
                                                                                    __Random.Next((int)CanvasSize.Width), __Random.Next((int)CanvasSize.Height))))
                {
                    new Boundary(0, 0, 0, (int)CanvasSize.Height),
                    new Boundary(0, 0, (int)CanvasSize.Width, 0),
                    new Boundary((int)CanvasSize.Width, 0, (int)CanvasSize.Width, (int)CanvasSize.Height),
                    new Boundary(0, (int)CanvasSize.Height, (int)CanvasSize.Width, (int)CanvasSize.Height)
                };
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);
            if (__Walls == null)
            {
                GenerateBoundaries();
            }
            SKCanvas _Canvas = e.Surface.Canvas;
            _Canvas.Clear();
            foreach (Boundary _Wall in __Walls)
            {
                _Canvas.DrawLine(_Wall.Origin.X, _Wall.Origin.Y, _Wall.End.X, _Wall.End.Y, __WhitePaint);
            }

            LightSource.Look(__Walls, _Canvas);
            _Canvas.DrawCircle(LightSource.Position.X, LightSource.Position.Y, 10, __RedPaint);
        }

        internal void TranslateLightSource(double totalX, double totalY)
        {
            LightSource.Position = Vector2.Add(__StartingPoint, new Vector2((float)(CanvasSize.Width * totalX / Width), (float)(CanvasSize.Height * totalY / Height)));
            InvalidateSurface();
        }
    }
}
