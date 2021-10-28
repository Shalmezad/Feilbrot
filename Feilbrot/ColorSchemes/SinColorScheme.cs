using System;
using SixLabors.ImageSharp.PixelFormats;

namespace Feilbrot.ColorSchemes
{
    public class SinColorScheme : IColorScheme
    {

        override public Rgba32 ColorForResult(int result, int iterations)
        {    
            if(result == -1){
                return new Rgba32(0,0,0,255);
            }
            else{
                float r = (float)Math.Sin(Math.PI * result * 10.0f / iterations);
                float g = (float)Math.Sin(Math.PI * result * 10.0f / iterations + Math.PI / 3.0f);
                float b = (float)Math.Cos(Math.PI * result * 10.0f / iterations);

                return new Rgba32(r, g, b, 1.0f);
            }
        }
    }
}
