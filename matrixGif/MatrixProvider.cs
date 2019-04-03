using matrixCore;

namespace matrixGif
{
    public class MatrixProvider
    {
        public static Matrix SetupMatrix(uint columnsAmount, uint rowsAmount, string argText)
        {
            var charactersCodes = new CharactersCodes(
                CharCodes.MinKatakana,
                CharCodes.MaxKatakana,
                32);

            var matrix = new Matrix(
                rowsAmount,
                columnsAmount,
                argText.ToMarksCentered(rowsAmount, columnsAmount),
                charactersCodes);
            return matrix;
        }
    }
}