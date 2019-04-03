using System;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;
using matrixCore;

namespace matrixGif
{
    internal static class ImageCreator
    {
        /// <summary>
        /// Returns a test matrix - half of it have a letter, the other half is empty
        /// </summary>
        internal static matrixCore.MatrixChar[][] Test(int rows, int columns){
            var result = new MatrixChar[columns][];
            
            for (int x = 0; x < columns; x++)
            {
                result[x] = new MatrixChar[rows];
                for (int y = 0; y < rows; y++)
                {
                    var c = 'T';
                    if (y > 10)
                        c = ' ';

                    result[x][y] = new MatrixChar(c, System.Drawing.Color.Green);
                }
            }

            return result;
        }

        internal static Image<Rgba32> CreateImage(
            this matrixCore.MatrixChar[][] array, 
            Size imageSize, 
            Font font,
             uint fontSize,
             uint latinCharOffset)
        {
            var img = new Image<Rgba32>(imageSize.Width, imageSize.Height);
            var painter = new Painter(font, img, imageSize, fontSize, latinCharOffset);
            painter.ColorBackground(Rgba32.Black);

            var columnsAmount = array.Length;
            var rowsAmount = array[0].Length; // Assume there is at least one column, will crash here if not

            for (int x = 0; x < columnsAmount; x++)
            {
                for (int y = 0; y < rowsAmount; y++)
                {
                    if (array[x][y].Char == ' '){
                        continue;
                    }
                    var color = array[x][y].Color.ToRgba32();
                    var pointToPrintAt = new Point(x, y);
                    painter.DrawLetter(array[x][y], color, pointToPrintAt);
                }
            }

            return img;
        }
    }
}