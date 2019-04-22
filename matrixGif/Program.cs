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
using System.Collections.Concurrent;
using System.Threading.Tasks;

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
            (IList<Image<Rgba32>> frames, float time) = DebugHelpers.MeasureTime<IList<Image<Rgba32>>>(() => GenerateImagesFromMatrix(imageSize, fontSize, font, matrix, latinCharOffset));
            log.Write($"generating the gif took {time} miliseconds");
            
            log.Write("saving the gif...");
            frames.SaveAsGif($"{outputDirectory}/{outputFileName}");
            log.Write("Done, cleaning up...");
            foreach (var frame in frames)
            {
                frame.Dispose();
            }
        }

        private static IList<Image<Rgba32>> Generate3TestFrames(
            Size imageSize, 
            uint fontSize,
             Font font,
              uint latinCharOffset,
              uint rowsAmount,
              uint columnsAmount){
            var i = 0;
            var frames = new List<Image<Rgba32>>();
            while (i < 3)
            {
                var mat = ImageCreator.Test((int)rowsAmount,(int)columnsAmount);
                Image<Rgba32> image = mat.CreateImage(imageSize, font, fontSize, latinCharOffset);
                frames.Add(image);
                i++;
            }
            return frames;
        } 

        private static IList<Image<Rgba32>> GenerateImagesFromMatrix(
            Size imageSize,
             uint fontSize, 
             Font font, 
             Matrix matrix, 
             uint latinCharOffset)
        {
            var matrixStates = new List<(MatrixChar[][] state, int index)>();
            var index = 0;
            while (true)
            {
                var raining = matrix.MoveState();
                matrixStates.Add(((MatrixChar[][])matrix.matrix.Clone(), index++));
                if (!raining) break;
            }

            var images = new ConcurrentBag<(Image<Rgba32>, int)>();

            Parallel.ForEach(matrixStates, stateIndex => {
                var image = stateIndex.Item1.CreateImage(imageSize, font, fontSize,latinCharOffset);
                images.Add((image, stateIndex.Item2));
                });

            return images.OrderBy(i => i.Item2).Select(i => i.Item1).ToList();
        }
    }
}
