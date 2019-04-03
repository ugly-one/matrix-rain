using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using matrixCore;

namespace matrixConsole
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            uint columns = 70;
            uint rows = 20;
            string messageToShow = "something went wrong - sorry!";
            string pathToDoneFile = Environment.CurrentDirectory;

            if (args.Length == 4)
            {
                columns = uint.Parse(args[0]);
                rows = uint.Parse(args[1]);
                messageToShow = args[2];
                pathToDoneFile = args[3];
            }

            var doneFilePath = $"{pathToDoneFile}/done.txt";

            DeleteDoneFile(doneFilePath);
            Console.CursorVisible = false;

            // the last character treated as "space". 
            // I had to chose a character from the collection of katakana characters so the width of it will be the same as other characters
            // taking "32" - normal space - gives a space with different width than japanese characters
            // Latin characters have different width than japanese :( .
            // So having a latin text mixed with japanese fucks eveyrthing up...
            var japaneseCodes = new CharactersCodes(
                CharCodes.MinKatakana, 
                CharCodes.MaxKatakana-1, 
                CharCodes.MaxKatakana); 

            var latinCodes = new CharactersCodes(65,122,32);
            var matrix = new Matrix(
                rows,
                columns,
                messageToShow.ToMarksCentered(rows, columns),
                japaneseCodes);
                
            while (true)
            {
                Console.Clear();
                var array = matrix.matrix;

                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < columns; x++)
                    {
                        // don't print if we're going to print a space. 
                        // We've cleared the console before anyway
                        // This assumes that we use Japanese characters :)
                        // When using all latin - we don't care, it's just for testing anyway
                        if (array[x][y].Char == CharCodes.MaxKatakana)
                            continue;
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = array[x][y].Color.ToConsoleColor();
                        Console.Write(array[x][y]);
                    }                    
                }

                var somethingChanged = matrix.MoveState();
                if (!somethingChanged)
                {
                    Thread.Sleep(2000);
                    CreateDoneFile(doneFilePath);
                    Thread.Sleep(1000); // to make sure we show something after the done-signal is sent
                    break;
                }

                Thread.Sleep(100);
            }
        }

        private static void DeleteDoneFile(string doneFilePath)
        {
            if (File.Exists(doneFilePath))
                File.Delete(doneFilePath);
        }

        private static void CreateDoneFile(string doneFilePath)
        {
            File.Create(doneFilePath);
        }
    }
}
