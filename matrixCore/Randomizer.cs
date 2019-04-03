using System;
namespace matrixCore
{
    public static class Randomizer
    {
        private static Random random = new Random((int)DateTime.Today.Ticks);

        public static int Get(int[] source)
        {
            return source[random.Next(0, source.Length)];
        }

        public static T GetRandomFrom<T>(T[] source)
        {
            return source[random.Next(0, source.Length)];
        }
    }
}
