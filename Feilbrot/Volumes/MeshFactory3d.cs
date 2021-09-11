using System.IO;
using Feilbrot.Graphics;
using System;

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
                Cube3d enclosingVolume = testable3D.EnclosingVolume();
                Point3d testPoint = new Point3d();

                // So, new trick:
                // We only care about the outer most points
                // So, we'll check each latitude/longitude, and march towards the center

                for(int latIdx = 0; latIdx < planeResolution; latIdx++){
                    for(int longIdx = 0; longIdx < planeResolution; longIdx++){
                        decimal latitude = 2.0M * (decimal)Math.PI * (latIdx * 1.0M / planeResolution) - (decimal)Math.PI;
                        decimal longitude = 2.0M * (decimal)Math.PI * (longIdx * 1.0M / planeResolution) - (decimal)Math.PI;
                        // We start from full distance:
                        decimal curDistance = 2.0M;
                        // And "march" towards center
                        while(curDistance > 0){
                            // https://stackoverflow.com/a/10475267/978509
                            // Make the point:
                            testPoint.x = curDistance * (decimal)Math.Cos((double)longitude) * (decimal)Math.Sin((double)latitude);
                            testPoint.y = curDistance * (decimal)Math.Sin((double)longitude) * (decimal)Math.Cos((double)latitude);
                            testPoint.z = curDistance * (decimal)Math.Cos((double)latitude);
                            // Test the point:
                            if(testable3D.PointInVolume(testPoint))
                                break;

                            curDistance -= marchResolution;
                        }
                        if(curDistance > 0){
                            // Save it
                            file.WriteLine($"{testPoint.x} {testPoint.y} {testPoint.z}");
                        }
                    }
                }

            } 
        }
    }
}