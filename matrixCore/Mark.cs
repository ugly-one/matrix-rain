using System;

namespace matrixCore
{
    public class Mark
    {
        public int X;
        public int Y;
        public char Value;
        private bool isPrintable;

        public Mark(uint x, uint y, char value)
        {
            X = (int)x;
            Y = (int)y;
            Value = value;
        }

        internal bool IsPrintable()
        {
            return isPrintable;
        }

        internal void Set()
        {
            isPrintable = true;
        }
    }
}