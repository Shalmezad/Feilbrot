using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

using Feilbrot.Brot2d;
using Feilbrot.Brot3d;
using Feilbrot.ColorSchemes;
using Feilbrot.Graphics;
using Feilbrot.Volumes;

namespace Feilbrot
{
    class Program
    {

        private static void BrotToImage(IMandel2d brot, int imgWidth, int imgHeight, int iterations, IColorScheme colorScheme, string imagePath="test.png")
        {
            
            ComplexRect2d window = brot.PreferredWindow();
            using(var image = new Image<Rgba32>(imgWidth, imgHeight))
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
                            row[x] = colorScheme.ColorForResult(result, iterations);
                        }
                    });

                    for(int x = 0; x < image.Width; x++)
                    {
                        pixelRowSpan[x] = row[x];
                    }
                }
                image.Save(imagePath); 
            }            
        }

        private static void BrotToPointCloud(IMandel3d brot, int resolution, int iterations =200)
        {
            using(StreamWriter file = new StreamWriter("testCloud.xyz"))
            {
                // xyz format is literally ascii xyz points
                var window = brot.PreferredWindow();

                for(int xIdx=0; xIdx < resolution; xIdx++)
                {
                    for(int yIdx=0; yIdx < resolution; yIdx++)
                    {
                        var points = new ConcurrentBag<ComplexPoint3d>();
                        var zIdxs = Enumerable.Range(0, resolution).ToList();
                        Parallel.ForEach(zIdxs, zIdx =>
                        {
                            decimal percX = xIdx * 1.0M / resolution;
                            decimal percY = yIdx * 1.0M / resolution;
                            decimal percZ = zIdx * 1.0M / resolution;
                            ComplexPoint3d testPoint = window.PointAtPercent(percX, percY, percZ);
                            int result = brot.PointInSet(testPoint, iterations);
                            if(result == -1){
                                // In the set:
                                points.Add(testPoint);
                            }
                        });
                        foreach(var point in points)
                        {
                            file.WriteLine($"{point.r} {point.i} {point.u}");
                        }
                    }
                }
            }
        }

        static Dictionary<Brot2dEnum, Type> Mandel2dCLIMap()
        {
            Dictionary<Brot2dEnum, Type> result = new Dictionary<Brot2dEnum, Type>();
            result.Add(Brot2dEnum.Chicken, typeof(Chickenbrot2d));
            result.Add(Brot2dEnum.Circle, typeof(Circlebrot2d));
            result.Add(Brot2dEnum.Mandlebrot, typeof(Mandelbrot2d));
            return result;
        }

        static Dictionary<ColorSchemeEnum, Type> ColorschemeCLIMap()
        {
            Dictionary<ColorSchemeEnum, Type> result = new Dictionary<ColorSchemeEnum, Type>();
            result.Add(ColorSchemeEnum.GrayCyan, typeof(GrayCyanColorScheme));
            result.Add(ColorSchemeEnum.Invert, typeof(InvertColorScheme));
            result.Add(ColorSchemeEnum.Sin, typeof(InvertColorScheme));
            return result;
        }

        static int Main(string[] args)
        {
            var rootCommand = new RootCommand
            {
                new Option<int>(
                    "--img-width",
                    getDefaultValue: () => 256
                ),
                new Option<int>(
                    "--img-height",
                    getDefaultValue: () => 256
                ),
                new Option<int>(
                    "--iterations",
                    getDefaultValue: () => 500
                ),
                new Option<Brot2dEnum>(
                    "--brotname",
                    getDefaultValue: () => Brot2dEnum.Chicken
                ),
                new Option<ColorSchemeEnum>(
                    "--colorname",
                    getDefaultValue: () => ColorSchemeEnum.GrayCyan
                )
            };

            rootCommand.Handler = CommandHandler.Create<int, int, int, Brot2dEnum, ColorSchemeEnum>((imgWidth, imgHeight, iterations, brotname, colorname) =>
            {
                Type brotClass = Mandel2dCLIMap()[brotname];
                IMandel2d brot = (IMandel2d)Activator.CreateInstance(brotClass);
                Type schemeClass = ColorschemeCLIMap()[colorname];
                IColorScheme colorscheme = (IColorScheme)Activator.CreateInstance(schemeClass);
                BrotToImage(brot, imgWidth, imgHeight, iterations, colorscheme, "test.png");
            });
            return rootCommand.InvokeAsync(args).Result;
        }
    }
}
