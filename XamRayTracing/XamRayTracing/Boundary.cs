using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Numerics;
using Xamarin.Forms;

namespace XamRayTracing
{
    public class Boundary
    {
        internal static SKPaint __BoundaryPaint = new SKPaint
        {
            Color = Color.Red.ToSKColor(),
            IsAntialias = true,
            StrokeWidth = 2
        };

        public Boundary(float x1, float y1, float x2, float y2)
        {
            Origin = new Vector2(x1, y1);
            End = new Vector2(x2, y2);
        }

        public Vector2 End { get; }
        public Vector2 Origin { get; }
    }
}
