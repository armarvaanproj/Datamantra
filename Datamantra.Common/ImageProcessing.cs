using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Datamantra.Common
{
    public static class ImageProcessing
    {
        public static List<Color> TenMostUsedColors { get; private set; }

        public static List<int> TenMostUsedColorIncidences { get; private set; }

        public static Color MostUsedColor { get; private set; }

        public static int MostUsedColorIncidence { get; private set; }

        private static int pixelColor;

        private static Dictionary<int, int> dctColorIncidence;

        #region Get most used Color based on color count repeated in each and every pixel
        public static Color GetMostUsedColor(Bitmap theBitMap)
        {
            TenMostUsedColors = new List<Color>();
            TenMostUsedColorIncidences = new List<int>();

            MostUsedColor = Color.Empty;
            MostUsedColorIncidence = 0;

            // does using Dictionary<int,int> here
            // really pay-off compared to using
            // Dictionary<Color, int> ?

            // would using a SortedDictionary be much slower, or ?

            dctColorIncidence = new Dictionary<int, int>();

            // this is what you want to speed up with unmanaged code
            for (int row = 0; row < theBitMap.Size.Width; row++)
            {
                for (int col = 0; col < theBitMap.Size.Height; col++)
                {
                    pixelColor = theBitMap.GetPixel(row, col).ToArgb();

                    if (dctColorIncidence.Keys.Contains(pixelColor))
                    {
                        dctColorIncidence[pixelColor]++;
                    }
                    else
                    {
                        dctColorIncidence.Add(pixelColor, 1);
                    }
                }
            }

            // note that there are those who argue that a
            // .NET Generic Dictionary is never guaranteed
            // to be sorted by methods like this
            var dctSortedByValueHighToLow = dctColorIncidence.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            // this should be replaced with some elegant Linq ?
            foreach (KeyValuePair<int, int> kvp in dctSortedByValueHighToLow.Take(10))
            {
                TenMostUsedColors.Add(Color.FromArgb(kvp.Key));
                TenMostUsedColorIncidences.Add(kvp.Value);
            }

            /// second filter to get top used color from top 10
            if (TenMostUsedColors.Count > 0)
            {
                if (dctSortedByValueHighToLow.Count < 2)    // this is for white and black images
                {
                    MostUsedColor = TenMostUsedColors[0];
                }
                else if (dctSortedByValueHighToLow.Count < 10)   // this is for major color combination of whit or black near by colors
                {
                    MostUsedColor = TenMostUsedColors.Where(a => a.R < 240 || a.G < 240 || a.B < 240).FirstOrDefault();
                }
                else if (dctSortedByValueHighToLow.Count < 50 || dctSortedByValueHighToLow.Count > 50)    /// this is to get the color which is from slightly darker color than white 
                {
                    MostUsedColor = TenMostUsedColors.Where(a => a.R < 230 && a.R > 30 || a.G < 230 && a.G > 30 || a.B < 230 && a.B > 30).FirstOrDefault();
                }
            }
            //return the msot used color
            return MostUsedColor;
        }

        #endregion

        #region Get Font Color from Background color
        public static String GetFontColorFromBackgroundColor(string value)
        {
            string fontColor = "rgb(0,0,0)";   // black color
            if (!string.IsNullOrWhiteSpace(value))
            {
                var items = value.Replace("rgb", "").Replace("(", "").Replace(")", "").Split(',').Select(k => byte.Parse(k)).ToArray();
               System.Windows.Media.Color color = System.Windows.Media.Color.FromRgb(items[0], items[1], items[2]);  // change the string to color

                var luminanace = 0.2126 * color.ScR + 0.7152 * color.ScG + 0.0722 * color.ScB;      // based on the luminanace value of luma calculation
                if (luminanace < 0.5)    // if less than 0.5 means it is dark color so change the font color to white color   else to black color
                {
                    fontColor = "rgb(255,255,255)";     //white color
                }
            }
            return fontColor;
        }
        #endregion


        #region get major color by average of RGB -- this mthod is not in use
        public static Color getDominantColor(Bitmap bmp)
        {
            //Used for tally
            int r = 0;
            int g = 0;
            int b = 0;

            int total = 0;

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color clr = bmp.GetPixel(x, y);

                    r += clr.R;
                    g += clr.G;
                    b += clr.B;

                    total++;
                }
            }

            //Calculate average
            r /= total;
            g /= total;
            b /= total;

            return Color.FromArgb(r, g, b);
        }
        #endregion
    }
}
