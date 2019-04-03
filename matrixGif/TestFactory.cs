using System.Collections.Generic;
using matrixCore;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;

namespace matrixGif
{
    public class TestFactory
    {
        public static void CreateTestImage(
            string outputDirectory, 
            string outputFileName, 
            Size imageSize, 
            uint fontSize, 
            Font font, 
            Matrix matrix,
            uint latinCharOffset)
        {
            for (int i = 0; i < 20; i++)
            {
                matrix.MoveState();
                Image<Rgba32> image = matrix.matrix.CreateImage(imageSize, font, fontSize, latinCharOffset);
                image.Save($"{outputDirectory}/{outputFileName}");
                image.Dispose();
            }
        }
    }
}