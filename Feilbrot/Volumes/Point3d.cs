using System;
using Feilbrot.Graphics;

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

        public static Point3d FromComplexPoint(ComplexPoint3d point3D)
        {
            return new Point3d(point3D.r, point3D.i, point3D.u);
        }
        public ComplexPoint3d ToComplexPoint()
        {
            return new ComplexPoint3d(x,y,z);
        }

        public override string ToString()
        {
            return $"Point3d#(x {x}, y: {y}, z: {z})";
        }
    }
}
