using System;

namespace Feilbrot.Volumes
{
    public class Cube3d
    {
        public Point3d point;
        public decimal width;
        public decimal height;
        public decimal depth;
        public Cube3d(Point3d point, decimal width=0, decimal height=0, decimal depth=0)
        {
            this.point = point;
            this.width = width;
            this.height = height;
            this.depth = depth;
        }

        public Point3d PointAtPercent(decimal percentX, decimal percentY, decimal percentZ)
        {
            Point3d result = new Point3d();
            result.x = percentX * width + point.x;
            result.y = percentY * height + point.y;
            result.z = percentZ * depth + point.z;
            return result;
        }

        public override string ToString()
        {
            return $"Cube3d#(point: {point}, width: {width}, height: {height}, depth: {depth})";
        }
    }
}
