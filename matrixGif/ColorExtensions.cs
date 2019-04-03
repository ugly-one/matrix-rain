using System.Drawing;
using SixLabors.ImageSharp.PixelFormats;

namespace matrixGif
{
    public static class ColorExtensions
    {
        public static Rgba32 ToRgba32 (this Color color){
            
            if (color == Color.Green)
                return Rgba32.LightGreen;
            if (color == Color.White)
                return Rgba32.White;
            if (color == Color.DarkGreen)
                return Rgba32.Green;
            if (color == Color.Black)
                return Rgba32.Black;
            return Rgba32.Red;
        }
    }
}