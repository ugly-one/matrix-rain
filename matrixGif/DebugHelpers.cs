using System;
using System.Diagnostics;

namespace matrixGif
{
    public static class DebugHelpers
    {
        public static (T result, float timeElapsed) MeasureTime<T>(Func<T> argFunc){
            var stopwatch = Stopwatch.StartNew();
            var result = argFunc();
            var time = stopwatch.ElapsedMilliseconds;
            return (result, time);
        }
        /// <summary>
        /// Im not sure how to made the generic MeasureTime return void, so I made an overload
        /// </summary>
        /// <param name="argFunc"></param>
        /// <returns></returns>
        public static float MeasureTime (Action argFunc){
            var stopwatch = Stopwatch.StartNew();
            argFunc();
            var time = stopwatch.ElapsedMilliseconds;
            return time;
        }
    }
}