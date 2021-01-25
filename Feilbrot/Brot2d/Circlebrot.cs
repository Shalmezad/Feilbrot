using Feilbrot.Graphics;

namespace Feilbrot.Brot2d
{
    class Circlebrot : IMandel2d
    {
        override protected ComplexPoint2d TransformPoint(ComplexPoint2d point2D, ComplexPoint2d initialPoint)
        {
            decimal a2 = point2D.r * point2D.r;
            decimal ab2 = point2D.r - point2D.i;
            decimal b2 = point2D.i * point2D.i;

            ComplexPoint2d result = new ComplexPoint2d();
            result.r = a2 + b2 + initialPoint.r;
            result.i = ab2 + initialPoint.i;
            return result;
        }

        override public ComplexRect2d PreferredWindow(){
            ComplexPoint2d point = new ComplexPoint2d(-1.5M, -1.3M);
            ComplexRect2d window = new ComplexRect2d(point, 2.0M, 2.0M);
            return window;
        }
    }
}