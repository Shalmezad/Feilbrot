
using SixLabors.ImageSharp.PixelFormats;

namespace Feilbrot.ColorSchemes
{
    public abstract class IColorScheme
    {

        public abstract Rgba32 ColorForResult(int result, int iterations);
    }
}