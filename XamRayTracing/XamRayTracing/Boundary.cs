using System.Numerics;

namespace XamRayTracing
{
    public class Boundary
    {
        public Boundary(float x1, float y1, float x2, float y2)
        {
            Origin = new Vector2(x1, y1);
            End = new Vector2(x2, y2);
        }

        public Vector2 End { get; }
        public Vector2 Origin { get; }
    }
}
