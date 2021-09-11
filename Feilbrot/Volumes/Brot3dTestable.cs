using Feilbrot.Brot3d;
using Feilbrot.Graphics;

namespace Feilbrot.Volumes
{
    public class Brot3dTestable : IPointTestable3d
    {
        private IMandel3d brot3d;
        private int iterations = 10;
        public Brot3dTestable(IMandel3d brot3d)
        {
            this.brot3d = brot3d;
        }

        override public bool PointInVolume(Point3d point3d)
        {
            return brot3d.PointInSet(point3d.ToComplexPoint(), iterations) == -1;
        }


        override public Cube3d EnclosingVolume(){
            ComplexCube3d viewCube = brot3d.PreferredWindow();
            //viewCube.point
            return new Cube3d(Point3d.FromComplexPoint(viewCube.point), viewCube.width, viewCube.height, viewCube.depth);
        }
    }
}