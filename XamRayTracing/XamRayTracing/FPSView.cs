using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamRayTracing
{
    public class FPSView : SKCanvasView
    {
        private const int FOCAL_DISTANCE_MULTIPLIER = 13;

        public List<float> WallDistances { get; private set; }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);
            if (WallDistances != null)
            {
                SKCanvas _Canvas = e.Surface.Canvas;
                _Canvas.Clear();
                float _WidthProportion = CanvasSize.Width / WallDistances.Count;
                for (int i = 0; i < WallDistances.Count; i++)
                {
                    var alphaMultiplier = 1 / WallDistances[i] * FOCAL_DISTANCE_MULTIPLIER;
                    SKRect rect = new SKRect();
                    rect.Size = new SKSize(_WidthProportion, CanvasSize.Height / WallDistances[i] * FOCAL_DISTANCE_MULTIPLIER);
                    rect.Location = new SKPoint(i * _WidthProportion, CanvasSize.Height/2 - rect.Height/2);

                    _Canvas.DrawRect(rect, new SKPaint
                    {
                        Color = Color.White.MultiplyAlpha(alphaMultiplier).ToSKColor(),
                        IsStroke = false,
                        IsAntialias = true
                    });
                }
            }
        }

        internal void Update(List<float> wallDistances)
        {
            WallDistances = wallDistances;
            InvalidateSurface();
        }
    }
}
