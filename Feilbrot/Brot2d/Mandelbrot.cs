using Feilbrot.Graphics;

namespace Feilbrot.Brot2d
{
    class Mandelbrot : IMandel2d
    {
        override protected ComplexPoint2d TransformPoint(ComplexPoint2d point2D, ComplexPoint2d initialPoint)
        {
            decimal a2 = point2D.r * point2D.r;
            decimal ab2 = point2D.r * point2D.i * 2;
            decimal b2 = point2D.i * point2D.i;

            ComplexPoint2d result = new ComplexPoint2d();
            result.r = a2 - b2 + initialPoint.r;
            result.i = ab2 + initialPoint.i;
            return result;
        }
    }
}