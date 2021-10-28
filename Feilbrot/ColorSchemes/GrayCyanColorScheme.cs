using SixLabors.ImageSharp.PixelFormats;

namespace Feilbrot.ColorSchemes
{
    public class GrayCyanColorScheme : IColorScheme
    {

        override public Rgba32 ColorForResult(int result, int iterations)
        {    
            if(result == -1){
                return new Rgba32(0,0,0,255);
            }
            else{
                // TODO: Should probably come up with a better way of making colors pop than multiplying by 10 causing it to hit the ceiling.
                float color = result * 10.0f / iterations;
                return new Rgba32(0.1f, color * 0.5f + 0.1f, color * 0.9f + 0.1f, 1.0f);
            }
        }
    }
}
