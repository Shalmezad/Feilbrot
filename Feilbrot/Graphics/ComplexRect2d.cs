using System;

namespace Feilbrot.Graphics
{
    class ComplexRect2d
    {
        public ComplexPoint2d point;
        public decimal width;
        public decimal height;
        public ComplexRect2d(ComplexPoint2d point, decimal width=0, decimal height=0)
        {
            this.point = point;
            this.width = width;
            this.height = height;
        }

        public override string ToString()
        {
            return $"ComplexRect2d#(point: {point}, width: {width}, height: {height})";
        }
    }
}
