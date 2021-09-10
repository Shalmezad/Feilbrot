using Feilbrot.Graphics;

namespace Feilbrot.Volumes
{
    public class Sphere : IPointTestable3d
    {
        private decimal radius;
        public Sphere(decimal radius=1.0M)
        {
            this.radius = radius;
        }

        override public bool PointInVolume(Point3d point3d)
        {
            decimal distance = 0;
            distance += point3d.x * point3d.x;
            distance += point3d.y * point3d.y;
            distance += point3d.z * point3d.z;
            return distance < radius * radius;
        }


        override public Cube3d EnclosingVolume(){
            Point3d origin = new Point3d(-radius, -radius, -radius);
            decimal diam = radius * 2;
            return new Cube3d(origin, diam, diam, diam);
        }
    }
}