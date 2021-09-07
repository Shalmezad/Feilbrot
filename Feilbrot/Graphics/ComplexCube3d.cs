using System;

namespace Feilbrot.Graphics
{
    public class ComplexCube3d
    {
        public ComplexPoint3d point;
        public decimal width;
        public decimal height;
        public decimal depth;
        public ComplexCube3d(ComplexPoint3d point, decimal width=0, decimal height=0, decimal depth=0)
        {
            this.point = point;
            this.width = width;
            this.height = height;
            this.depth = depth;
        }

        public ComplexPoint3d PointAtPercent(decimal percentX, decimal percentY, decimal percentZ)
        {
            ComplexPoint3d result = new ComplexPoint3d();
            result.r = percentX * width + point.r;
            result.i = percentY * height + point.i;
            result.u = percentZ * depth + point.u;
            return result;
        }

        public override string ToString()
        {
            return $"ComplexCube3d#(point: {point}, width: {width}, height: {height}, depth: {depth})";
        }
    }
}
