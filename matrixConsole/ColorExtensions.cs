using System;
using System.Collections.Generic;
using System.Drawing;

namespace matrixConsole
{
    public static class ColorExtensions
    {
        public static ConsoleColor ToConsoleColor(this Color argColor){
            
            var map = new Dictionary<Color,ConsoleColor>(){
                {Color.Green, ConsoleColor.Green},
                {Color.DarkGreen, ConsoleColor.DarkGreen},
                {Color.Black, ConsoleColor.Black},
                {Color.White, ConsoleColor.White},
            };
            
            var exists = map.TryGetValue(argColor, out var color);
            if (!exists) return ConsoleColor.White;

            return color;
        }
    }
}