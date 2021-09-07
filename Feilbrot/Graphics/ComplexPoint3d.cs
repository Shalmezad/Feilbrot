using System;

namespace Feilbrot.Graphics
{
    public struct ComplexPoint3d
    {

        public decimal r;
        public decimal i;
        public decimal u;
        public ComplexPoint3d(decimal r=0, decimal i=0, decimal u=0)
        {
            this.r = r;
            this.i = i;
            this.u = u;
        }

        public override string ToString()
        {
            return $"ComplexPoint3d#(r: {r}, i: {i}, u: {u})";
        }
    }
}
