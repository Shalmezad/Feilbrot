using System;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

using Feilbrot.Brot2d;
using Feilbrot.Graphics;

namespace Feilbrot
{
    class Program
    {
        static void Main(string[] args)
        {
            IMandel2d brot = new Circlebrot();
            ComplexRect2d window = brot.PreferredWindow();

            int width = 640;
            int height = 480;


            using(var image = new Image<Rgba32>(width, height))
            {
                for(int y = 0; y < image.Height; y++)
                {
                    Span<Rgba32> pixelRowSpan = image.GetPixelRowSpan(y);
                    for(int x = 0; x < image.Width; x++)
                    {
                        // Convert image x/y to window r/i
                        decimal percX = x * 1.0M / image.Width;
                        decimal percY = y * 1.0M / image.Height;
                        ComplexPoint2d testPoint = window.PointAtPercent(percX, percY);
                        int result = brot.PointInSet(testPoint, 100);
                        if(result == -1){
                            pixelRowSpan[x] = new Rgba32(0,0,0,255);
                        }
                        else{
                            float color = result / 100f;
                            pixelRowSpan[x] = new Rgba32(color, color, 0.5f, 1.0f);
                        }
                    }
                }
                image.Save("test.png"); 
            }
        }
    }
}
