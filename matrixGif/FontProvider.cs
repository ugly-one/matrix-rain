using System.IO;
using System.Reflection;
using SixLabors.Fonts;

namespace matrixGif
{
    public class FontProvider
    {
        public static Font LoadFont(uint fontSize)
        {
            var fontCollection = new FontCollection();
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fontFamily = fontCollection.Install($"{assemblyFolder}/resources/ipam.ttf");
            var font = new Font(fontFamily, fontSize);
            return font;
        }
    }
}