using System.Drawing;

namespace matrixCore
{
    public class ResultChar : MatrixChar
    {
        public ResultChar(char argChar, Color argColor) : base(argChar, argColor)
        {
        }
    }

    public class MatrixChar
    {
        public MatrixChar(char argChar, Color argColor)
        {
            Char = argChar;
            Color = argColor;
        }

        public char Char { get; }
        public Color Color { get; }

        public override string ToString() => Char.ToString();
    }
}