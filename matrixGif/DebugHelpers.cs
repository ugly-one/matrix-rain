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
    }
}