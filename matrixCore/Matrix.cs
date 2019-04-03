using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace matrixCore
{
    public class Matrix
    {
        public MatrixChar[][] matrix;
        private CharactersCodes characterCodes;
        private uint rows;
        private uint columns;
        private Mark[] marks;
        private Drop[] drops;
        private Random randomGenerator;

        public Matrix(uint rows, uint columns, IEnumerable<Mark> marks, CharactersCodes charCodes)
        {
            this.characterCodes = charCodes;
            this.rows = rows;
            this.columns = columns;
            this.marks = new Mark[columns];
            foreach (var m in marks)
            {
                this.marks[m.X] = m; // place the requested mark at the index representing its X
            }

            drops = new Drop[columns];
            randomGenerator = new Random((int)DateTime.Now.Ticks);
            ResetMatrix();
        }

        private void ResetMatrix()
        {
            matrix = new MatrixChar[columns][];

            for (int x = 0; x < columns; x++)
            {
                matrix[x] = new MatrixChar[rows];
                for (int y = 0; y < rows; y++)
                {
                    matrix[x][y] = new MatrixChar((char)characterCodes.SpaceCode, Color.Black);
                }
            }
        }

        public bool MoveState()
        {
            AdvanceState();
            ResetMatrix();
            bool dropsFalling = false;
            //print drops
            foreach (var drop in drops)
            {
                if (drop is null) continue;
                PrintDrop(drop);
                dropsFalling = true;
            }

            // print marks if possible
            foreach (var mark in marks.Where(m => !(m is null)))
            {
                if (mark.IsPrintable())
                    PrintMark(mark);
            }

            return dropsFalling;

        }

        private bool IsDone()
        {
            if (marks.All(m => m is null)) return false;
            foreach (var mark in marks)
            {
                if (mark is null) continue;
                if (!mark.IsPrintable()) return false;
            }
            return true;
        }

        private void PrintMark(Mark mark)
        {
            Print(mark.X, mark.Y, new ResultChar(mark.Value, Color.White));
        }

        public void PrintDrop(Drop drop)
        {
            // print head
            var randomChar = GetRandomChar();
            Print(drop.X, drop.Y, new MatrixChar(randomChar, Color.White));

            // print tail
            int y = drop.Y - 1;
            int length = drop.Y - y;

            while (length < drop.Length && y >= 0)
            {
                var color = Randomizer.GetRandomFrom(
                    new Color[] { 
                        Color.DarkGreen, 
                        Color.Green });
                Print(drop.X, y, new MatrixChar(GetRandomChar(), color));
                y--;
                length = drop.Y - y;
            }
        }

        private char GetRandomChar()
        {
            return (char)randomGenerator.Next((int)characterCodes.MinCharCode, (int)characterCodes.MaxCharCode);
        }

        /// <summary>
        /// Does not print if the coordinates are outside of the matrix area.
        /// Marks drops as available for printing if current drop goes through mark's position
        /// </summary>
        private void Print(int x, int y, MatrixChar argChar)
        {
            if ((x > columns - 1) || (y > rows - 1)) return;

            if (!(marks[x] is null))
            {
                if (marks[x].Y == y)
                    marks[x].Set();
            }

            matrix[x][y] = argChar;
        }

        private void AdvanceState()
        {
            for (int i = 0; i < columns; i++)
            {
                if (drops[i] is null) continue;

                drops[i].Move();

                // remove drops that are outside -> Y coordinate of their tails is bigger than amount of rows
                if (drops[i].YTail >= rows)
                    drops[i] = null;
            }

            // if we are not done start 2 new drops somewhere
            if (!IsDone())
            {
                TryAddDrop();
                TryAddDrop();
            }
        }

        private void TryAddDrop()
        {
            var tryLimit = 4;

            while (true)
            {
                var dropColumn = 0;

                // increase possibility of picking the right column (with a mark)
                var rightColumnProbality = randomGenerator.Next(0, 5);
                if (rightColumnProbality == 1)
                    dropColumn = Randomizer.GetRandomFrom(marks.Where(m => m != null && !m.IsPrintable()).ToArray()).X;
                else
                    dropColumn = randomGenerator.Next(0, (int)columns);

                var dropLength = randomGenerator.Next(10, 20);

                // check if there is already a mark on the column we want to add a drop.
                // if there is nothing - add a drop

                if (marks[dropColumn] is null || !marks[dropColumn].IsPrintable())
                {
                    if (drops[dropColumn] is null)
                    {
                        drops[dropColumn] = new Drop(dropColumn, dropLength);
                        break;
                    }
                }
                tryLimit++;
                if (tryLimit >= 4) break;
            }

        }
    }
}
