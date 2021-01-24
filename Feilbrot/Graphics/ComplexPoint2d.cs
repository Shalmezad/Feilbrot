using System;

namespace Feilbrot.Graphics
{
    struct ComplexPoint2d
    {

        public decimal r;
        public decimal i;
        public ComplexPoint2d(decimal r=0, decimal i=0)
        {
            this.r = r;
            this.i = i;
        }

        public override string ToString()
        {
            return $"ComplexPoint2d#(r: {r}, i: {i})";
        }
    }
}
