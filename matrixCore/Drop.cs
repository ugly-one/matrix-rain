using System;

namespace matrixCore
{
    public class Drop
    {
        public Drop(int x, int length)
        {
            Y = 0;
            X = x;
            Length = length;
        }

        public int Y { get; private set; }
        public int X { get;}
        public int Length { get; }
        public int YTail => Y - Length;
        internal void Move()
        {
            Y++;
        }
    }
}