using System;
using matrixCore;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace matrixGif
{
    internal class Painter
    {
        private Font font;
        private Image<Rgba32> img;
        private readonly Size imageSize;
        private readonly uint fontSize;
        private PointTransformer pointTransformer;
        private uint latinCharOffset;

        public Painter(
            Font font, 
            Image<Rgba32> img, 
            Size imageSize,
            uint fontSize,
            uint latinCharOffset)
        {
            this.font = font;
            this.img = img;
            this.imageSize = imageSize;
            this.fontSize = fontSize;
            this.pointTransformer = new PointTransformer(imageSize, fontSize);
            this.latinCharOffset = latinCharOffset;
        }

        internal void ColorBackground(Rgba32 black)
        {
            img.Mutate(
                ctx => ctx
                .Fill(Rgba32.Black));
        }

        internal void DrawLetter(MatrixChar argText, Rgba32 color, Point point)
        {
            Point imagePoint = pointTransformer.Translate(point);
            if (argText is ResultChar)
                imagePoint.Offset((int)latinCharOffset,0);
            img.Mutate(ctx => ctx.DrawText(argText.ToString(), font, color, imagePoint));
        }
    }
}