using System;
using System.IO;
using System.Linq;

namespace matrixGif
{
    public class Log
    {
        private StreamWriter file;
        string logName = "GifRain.log";

        public Log()
        {
            file = new System.IO.StreamWriter($"{Environment.CurrentDirectory}/{logName}", true);
        }

        internal void Write(string message) {
            file = new System.IO.StreamWriter($"{Environment.CurrentDirectory}/{logName}", true);
            file.WriteLine(message);
            file.Dispose();
            Console.WriteLine(message);
        }
        internal void Write(string[] message) {
            file = new System.IO.StreamWriter($"{Environment.CurrentDirectory}/{logName}", true);
            file.WriteLine(message.Aggregate((s1, s2) => $"{s1}, {s2}"));
            file.Dispose();
            Console.WriteLine(message);
        } 
    }
}