using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.Fonts;
using SixLabors.Primitives;
using System.IO;
using System.Linq;
using matrixCore;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace matrixGif
{
    class Program
    {

        static Log log;
        static void Main(string[] args)
        {
            log = new Log();
            log.Write("Start");
            log.Write(args);
            var outputDirectory = args[0];
            var messageToBeShown = args[1];
            var outputFileName = "result.gif";

            
            var columnsAmount = Math.Clamp((uint)messageToBeShown.Length + 4,10,40);
            if (messageToBeShown.Length % 2 != 0)
                columnsAmount += 1;
            var rowsAmount = (uint)((float)(columnsAmount * 16/9) + 1);

            var imageSize = new Size(576,1024);
            var pixelsPerChar = 576 / columnsAmount;
            var latinCharOffset = (uint)pixelsPerChar / 4;

            uint fontSize = (uint)pixelsPerChar;
            Font font = FontProvider.LoadFont(fontSize);
            Matrix matrix = MatrixProvider.SetupMatrix(columnsAmount, rowsAmount, messageToBeShown);

            log.Write("crating gif...");
            //CreateTestImage(outputDirectory, outputFileName, imageSize, fontSize, font, matrix);
            CreateGif(
                outputDirectory,
                outputFileName,
                imageSize,
                fontSize,
                font,
                matrix,
                latinCharOffset);
            log.Write("DONE");
        }

        private static void CreateGif(
            string outputDirectory, 
            string outputFileName, 
            Size imageSize, 
            uint fontSize, 
            Font font, 
            Matrix matrix,
            uint latinCharOffset)
        {
            (IList<Image<Rgba32>> frames, float time) = DebugHelpers.MeasureTime<IList<Image<Rgba32>>>(() => GenerateImagesFromMatrix(imageSize, fontSize, font, matrix, frames, latinCharOffset));
            log.Write($"generating the gif took {time} miliseconds");
            
            log.Write("saving the gif...");
            frames.SaveAsGif($"{outputDirectory}/{outputFileName}");
            log.Write("Done, cleaning up...");
            foreach (var frame in frames)
            {
                frame.Dispose();
            }
        }

        private static void Generate3TestFrames(
            Size imageSize, 
            uint fontSize,
             Font font,
              IList<Image<Rgba32>> frames,
              uint latinCharOffset,
              uint rowsAmount,
              uint columnsAmount){
            var i = 0;
            while (i < 3)
            {
                var mat = ImageCreator.Test((int)rowsAmount,(int)columnsAmount);
                Image<Rgba32> image = mat.CreateImage(imageSize, font, fontSize, latinCharOffset);
                frames.Add(image);
                i++;
            }
        } 

        private static IList<Image<Rgba32>> GenerateImagesFromMatrix(
            Size imageSize,
             uint fontSize, 
             Font font, 
             Matrix matrix, 
             uint latinCharOffset)
        {
            uint frameNumber = 0;
            var frames = new List<Image<Rgba32>>();            
            while (true)
            {
                var raining = matrix.MoveState();
                if (!raining) break;
                (Image<Rgba32> image, float time) = DebugHelpers.MeasureTime<Image<Rgba32>>(() => matrix.matrix.CreateImage(imageSize, font, fontSize,latinCharOffset));
                log.Write($"time elapsed {time}");
                frames.Add(image);
                log.Write($"created image/frame nr {frameNumber}");
                frameNumber++;
            }
            return frames;
        }
    }
}
