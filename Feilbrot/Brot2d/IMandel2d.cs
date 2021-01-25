using Feilbrot.Graphics;

namespace Feilbrot.Brot2d
{
    public abstract class IMandel2d
    {

        public int PointInSet(ComplexPoint2d point2D, int iterations)
        {
            ComplexPoint2d initialPoint = point2D;
            for(int i=0; i< iterations; i++){
                point2D = TransformPoint(point2D, initialPoint);
                decimal distance = point2D.r * point2D.r + point2D.i * point2D.i;
                if(distance > 4){
                    return i;
                }
            }
            return -1;
        }
        protected abstract ComplexPoint2d TransformPoint(ComplexPoint2d point2D, ComplexPoint2d initialPoint);
        public abstract ComplexRect2d PreferredWindow();
    }
}