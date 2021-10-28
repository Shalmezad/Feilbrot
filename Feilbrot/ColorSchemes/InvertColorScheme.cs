using SixLabors.ImageSharp.PixelFormats;

namespace Feilbrot.ColorSchemes
{
    public class InvertColorScheme : IColorScheme
    {

        override public Rgba32 ColorForResult(int result, int iterations)
        {    
            if(result == -1){
                return new Rgba32(255,255,255,255);
            }
            else{
                // TODO: Should probably come up with a better way of making colors pop than multiplying by 10 causing it to hit the ceiling.
                float color = 1.0f - (result * 10.0f / iterations);
                return new Rgba32(color, color, color, 1.0f);
            }
        }
    }
}
