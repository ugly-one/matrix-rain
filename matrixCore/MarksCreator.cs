using System.Collections.Generic;
using System.Linq;

namespace matrixCore
{
    public static class MarksCreator
    {
        /// <summary>
        /// </summary>
        /// <returns>The marks.</returns>
        /// <param name="input">Input.</param>
        /// <param name="rowId">Row identifier.</param>
        /// <param name="columnId">Column identifier.</param>
        public static IEnumerable<Mark> ToMarks(this string input, uint rowId, uint columnId)
        {
            return input.Select(c => new Mark(columnId++, rowId, c));
        }

        public static IEnumerable<Mark> ToMarksCentered(this string input, uint rows, uint columns)
        {
            var rowId = rows / 2;
            var columnId = columns / 2 - (uint)input.Length / 2;
            if (columnId < 0) columnId = 0;
            return input.ToMarks(rowId, columnId).Take((int)columns);
        }
    }
}
