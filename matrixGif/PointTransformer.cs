using System;
using SixLabors.Primitives;

namespace matrixGif
{
    internal class PointTransformer
    {
        private Size imageSize;
        private uint fontSize;

        public PointTransformer(Size imageSize, uint fontSize)
        {
            this.imageSize = imageSize;
            this.fontSize = fontSize;
        }

        internal Point Translate(Point point)
        {
            return new Point((int)(point.X * fontSize), (int)(point.Y * fontSize));
        }
    }
}