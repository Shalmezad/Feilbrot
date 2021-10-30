using Feilbrot.Graphics;

namespace Feilbrot.Brot2d
{
    public class Testbrot2d : IMandel2d
    {
        override protected ComplexPoint2d TransformPoint(ComplexPoint2d point2D, ComplexPoint2d initialPoint)
        {
            decimal a2 = point2D.r * point2D.r;
            decimal ab2 = point2D.r * point2D.i * 2;
            decimal b2 = point2D.i * point2D.i;

            ComplexPoint2d result = new ComplexPoint2d();
            result.r = (ab2 / a2 / 3.0M) + a2 * b2 + initialPoint.r;
            result.i = (ab2 * b2 / 1.8M) - a2 * b2 + initialPoint.i;
            return result;
        }

        override public ComplexRect2d PreferredWindow(){
            ComplexPoint2d point = new ComplexPoint2d(-3.0M, -1.8M);
            ComplexRect2d window = new ComplexRect2d(point, 5.5M, 3.5M);
            return window;
        }
    }
}