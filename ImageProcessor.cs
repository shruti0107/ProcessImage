using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessImage
{
    public class ImageProcessor
    {
        private static Dictionary<int, int> dctColorIncidence;

        private static Dictionary<string,string> GetPredefinedColors()
        {
            Dictionary<string, string> colorList = new Dictionary<string, string>();
            colorList.Add("Black", "0, 0, 0");
            colorList.Add("White", "255, 255, 255");
            colorList.Add("Red", "255, 0, 0");
            colorList.Add("Lime", "0, 255, 0");
            colorList.Add("Blue", "0, 0, 255");
            colorList.Add("Yellow", "255, 255, 0");
            colorList.Add("Cyan / Aqua", "0, 255, 255");
            colorList.Add("Magenta", "255, 0, 255");
            colorList.Add("Silver", "192, 192, 192");
            colorList.Add("Gray", "128, 128, 128");
            colorList.Add("Maroon", "128, 0, 0");
            colorList.Add("Olive", "128, 128, 0");
            colorList.Add("Green", "0, 128, 0");
            colorList.Add("Purple", "128, 0, 128");
            colorList.Add("Teal", "0, 128, 128");
            colorList.Add("Navy", "0, 0, 128");
            //colorList.Add("Test Shruti", "25, 77, 197");
            return colorList;
        }
        public static Color GetMostUsedColor(Bitmap image)
        {
            int x, y;
            dctColorIncidence = new Dictionary<int, int>();

            // Loop through the images pixels to reset color.
            for (x = 0; x < image.Width; x++)
            {
                for (y = 0; y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int pixel=pixelColor.ToArgb();
                    if (dctColorIncidence.Keys.Contains(pixel))
                        dctColorIncidence[pixel]++;
                    else
                        dctColorIncidence.Add(pixel, 1);
                }
            }

            return Color.FromArgb(dctColorIncidence.OrderByDescending(a => a.Value).ToList()[0].Key);
        }

        public static double CalculateDistance(Color current, string match)
        {
            int redDifference;
            int greenDifference;
            int blueDifference;

            string[] sMatch= match.Split(',');

            redDifference = current.R - Convert.ToInt32(sMatch[0].ToString());
            greenDifference = current.G - Convert.ToInt32(sMatch[1].ToString());
            blueDifference =current.B - Convert.ToInt32(sMatch[2].ToString());

            return Math.Sqrt(Math.Pow(redDifference,2) + Math.Pow(greenDifference,2) + Math.Pow(blueDifference,2));
        }

        public static string FindNearestColor(Bitmap image)
        {
            string nearestColor = "";
            double nearestColorDistance = 0.0;

            Color current = GetMostUsedColor(image);
            Dictionary<string, string> colorList = GetPredefinedColors();

            for (int i = 0; i < colorList.Count; i++)
            {
                double distance = CalculateDistance(current, colorList.ElementAt(i).Value);
                if (string.IsNullOrEmpty(nearestColor))
                {
                    nearestColorDistance = distance;
                    nearestColor = colorList.ElementAt(i).Key;
                }
                if (nearestColorDistance > distance)
                {
                    nearestColorDistance = distance;
                    nearestColor = colorList.ElementAt(i).Key;
                }
            }
            if (nearestColorDistance > 100)
                nearestColor = "Cannot find nearest color in the pre-defined color set";
            return nearestColor;
        }
    }
}
