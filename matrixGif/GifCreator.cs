using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;
using SixLabors.ImageSharp.Formats.Gif;
using System.Linq;

namespace matrixGif
{
    internal static class GifCreator
    {
        internal static void SaveAsGif(this IList<Image<Rgba32>> argImages, string argPath)
        {
            if (argImages.Count == 0) return;
            var stream = new FileStream(argPath, FileMode.CreateNew);
            var gif = argImages[1].Clone();
            foreach (var image in argImages.Skip(2))
            {
                gif.Frames.AddFrame(image.Frames[0]);
            }

            gif.Save(stream, new GifEncoder());
            gif.Dispose();
            stream.Close();
        }
    }
}