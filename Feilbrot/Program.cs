using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            IMandel2d brot = new Chickenbrot();
            ComplexRect2d window = brot.PreferredWindow();

            int width = 128;
            int height = 128;
            int iterations = 500;


            using(var image = new Image<Rgba32>(width, height))
            {
                for(int y = 0; y < image.Height; y++)
                {
                    Span<Rgba32> pixelRowSpan = image.GetPixelRowSpan(y);

                    Rgba32[] row = new Rgba32[image.Width];
                    var source = Enumerable.Range(0, image.Width).ToArray();
                    var rangePartitioner = Partitioner.Create(0, source.Length);

                    Parallel.ForEach(rangePartitioner, (range, loopState) =>
                    {
                        for(int x = range.Item1; x < range.Item2; x++)
                        {
                            // Convert image x/y to window r/i
                            decimal percX = x * 1.0M / image.Width;
                            decimal percY = y * 1.0M / image.Height;
                            ComplexPoint2d testPoint = window.PointAtPercent(percX, percY);
                            int result = brot.PointInSet(testPoint, iterations);
                            if(result == -1){
                                row[x] = new Rgba32(0,0,0,255);
                            }
                            else{
                                // TODO: Should probably come up with a better way of making colors pop than multiplying by 10 causing it to hit the ceiling.
                                float color = result * 10.0f / iterations;
                                row[x] = new Rgba32(0.1f, color * 0.5f + 0.1f, color * 0.9f + 0.1f, 1.0f);
                            }
                        }
                    });

                    for(int x = 0; x < image.Width; x++)
                    {
                        pixelRowSpan[x] = row[x];
                    }
                }
                image.Save("test.png"); 
            }
        }
    }
}
