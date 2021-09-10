using Feilbrot.Graphics;

namespace Feilbrot.Volumes
{
    public abstract class IPointTestable3d
    {

        public abstract bool PointInVolume(Point3d point3d);
        public abstract Cube3d EnclosingVolume();
    }
}