using Feilbrot.Graphics;

namespace Feilbrot.Brot3d
{
    public class Chickenbrot3d : IMandel3d
    {
        override protected ComplexPoint3d TransformPoint(ComplexPoint3d point3D, ComplexPoint3d initialPoint)
        {
            decimal a2 = point3D.r * point3D.r;
            decimal ab2 = point3D.r * point3D.i;
            decimal b2 = point3D.i * point3D.i;
            decimal bc2 = point3D.i * point3D.u;
            decimal c2 = point3D.u * point3D.u;

            ComplexPoint3d result = new ComplexPoint3d();
            result.r = a2 - b2 + initialPoint.r;
            result.i = ab2 + initialPoint.i;
            result.u = c2 - bc2 + initialPoint.u;
            return result;
        }

        override public ComplexCube3d PreferredWindow(){
            ComplexPoint3d point = new ComplexPoint3d(-2.5M, -1.5M, -2.0M);
            ComplexCube3d window = new ComplexCube3d(point, 3.5M, 3.5M, 4.0M);
            return window;
        }
    }
}