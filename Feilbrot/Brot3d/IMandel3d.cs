using System;
using Feilbrot.Graphics;

namespace Feilbrot.Brot3d
{
    public abstract class IMandel3d
    {

        public int PointInSet(ComplexPoint3d point3D, int iterations)
        {
            ComplexPoint3d initialPoint = point3D;
            for(int i=0; i< iterations; i++){
                try {
                    point3D = TransformPoint(point3D, initialPoint);
                    // TODO: Verify distance for 3d point:
                    decimal distance = point3D.r * point3D.r + point3D.i * point3D.i + point3D.u * point3D.u;
                    if(distance > 15){
                        return i;
                    }
                }
                catch(DivideByZeroException){
                    return i;
                }
                catch(OverflowException){
                    return i;
                }
            }
            return -1;
        }
        protected abstract ComplexPoint3d TransformPoint(ComplexPoint3d point3D, ComplexPoint3d initialPoint);
        public abstract ComplexCube3d PreferredWindow();
    }
}