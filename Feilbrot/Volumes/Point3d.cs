using System;

namespace Feilbrot.Volumes
{
    public struct Point3d
    {

        public decimal x;
        public decimal y;
        public decimal z;
        public Point3d(decimal x=0, decimal y=0, decimal z=0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override string ToString()
        {
            return $"Point3d#(x {x}, y: {y}, z: {z})";
        }
    }
}
