using System.IO;
using Feilbrot.Graphics;

namespace Feilbrot.Volumes
{
    public class MeshFactory3d
    {

        int planeResolution = 100;
        decimal marchResolution = 0.001M;
        public MeshFactory3d()
        {

        }
        public void TestableToMesh(IPointTestable3d testable3D){
            
            using(StreamWriter file = new StreamWriter("testCloud.xyz"))
            {
                // Ok, so now the fun part:
                // We're going to try starting from each major plane (xy, yz, xz) from both directions (+/-)
                // And "march" until we hit a point
                // By getting points from the outside of each plane, we should hopefully get a rough approximation of the shape
                Cube3d enclosingVolume = testable3D.EnclosingVolume();
                Point3d testPoint = new Point3d();

                // 1) Walk X/Y from +
                for(int xIdx = 0; xIdx < planeResolution; xIdx ++){
                    for(int yIdx = 0; yIdx < planeResolution; yIdx++){
                        testPoint = enclosingVolume.PointAtPercent(xIdx * 1.0M / planeResolution, 
                                                                   yIdx * 1.0M / planeResolution,
                                                                   0.0M);
                        decimal marchDistance = enclosingVolume.point.z;
                        while(marchDistance < enclosingVolume.point.z + enclosingVolume.depth)
                        {
                            marchDistance += marchResolution;
                            testPoint.z = enclosingVolume.point.z + marchDistance;
                            if(testable3D.PointInVolume(testPoint))
                                break;
                        }
                        if(marchDistance < enclosingVolume.point.z + enclosingVolume.depth)
                        {
                            // Record it
                            file.WriteLine($"{testPoint.x} {testPoint.y} {marchDistance}");
                        }
                    }
                }
            } 
        }
    }
}