using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace ASCII_art_project
{
    class Program
    {
        /// <summary>
        /// https://www.youtube.com/watch?v=55iwMYv8tGI&t=275s
        /// https://play.ertdfgcvb.xyz/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //const string density = "Ñ@#W$9876543210?!abc;:+=-,._ ";
            const string density = "Ñ@#W$9876543210?!abc;:+=-,._ ";

            //string s = @"C:\Users\johnm\source\repos\ASCII_art_solution\images\test3.jpg";
            string s = @"C:\Users\johnm\source\repos\ASCII_art_solution\images\ted1.jpg";
            //string s = @"C:\Users\johnm\source\repos\ASCII_art_solution\images\bear2.jpg";
            //string s = @"C:\Users\johnm\source\repos\ASCII_art_solution\images\test2.jpg";
            //string s = @"C:\Users\johnm\source\repos\ASCII_art_solution\images\test1.jpg";
            //string s = @"C:\Users\johnm\source\repos\ASCII_art_solution\images\testdogs.jpg";
            //string s = @"C:\Users\johnm\source\repos\ASCII_art_solution\images\k.jpg";
            //string s = @"C:\Users\johnm\source\repos\ASCII_art_solution\images\GoogleLogo.jpg";
            //string s = @"C:\Users\johnm\source\repos\ASCII_art_solution\images\255red.png";

            bool inverted = false;

            Image i = Image.FromFile(s);

            Console.WriteLine($"width: {i.Width} height: {i.Height}");

            //List<Color> ColourArray = GetColourArray(i);

            ImageContainer imageContainer = GetColourArray(i);

            Console.WriteLine($"min colour {imageContainer.max} max colour {imageContainer.max}");

            int widthCount = 0;

            List<string> output = new();
            string line = "";

            foreach(Color c in imageContainer.colours)
            {
                widthCount++;

                int greyScaleColour = (c.R + c.G + c.B) / 3;

                double index = 0;
                
                if(inverted)
                    index = Math.Floor(Map(greyScaleColour, 0, 255,  density.Length - 1, 0));
                else
                    index = Math.Floor(Map(greyScaleColour, 0, 255, 0, density.Length - 1));
                

                /*
                if(inverted)
                    index = Math.Floor(Map(greyScaleColour, imageContainer.min, imageContainer.max,  density.Length - 1, 0));
                else
                    index = Math.Floor(Map(greyScaleColour, imageContainer.min, imageContainer.max, 0, density.Length - 1));
                */

                //string charToAdd = density[(int)index] == ' ' ? "&nbsp" : density[(int)index];
                //line += density[(int)index];

                //Console.WriteLine((int)index);
                line += density[(int)index] == ' ' ? "&nbsp" : density[(int)index];

                if (widthCount == i.Width)
                {
                    line += "<br>";
                    widthCount = 0;
                    output.Add(line);
                    line = "";
                }
            }

            Console.WriteLine("before write to file");

            using (StreamWriter writetext = new StreamWriter(@"C:\Users\johnm\source\repos\ASCII_art_solution\images\output.txt"))
            {
                foreach(string linetext in output)
                {
                    writetext.WriteLine(linetext);
                }
            }
        }
        /// <summary>
        /// 
        /// https://forum.unity.com/threads/re-map-a-number-from-one-range-to-another.119437/
        /// </summary>
        /// <returns></returns>
        public static float Map(float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        static public ImageContainer GetColourArray(Image sourceImage)
        {
            /// xhttps://social.msdn.microsoft.com/Forums/vstudio/en-US/ad60ec35-fe58-4d76-aa91-8da57e3abeff/how-to-get-rgb-array-from-an-systemdrawingimage-on-c?forum=csharpgeneral
            List<Color> colors = new List<Color>();
            ImageContainer imageContainer = new();

            using (Bitmap bmp = new Bitmap(sourceImage))
            using (Bitmap redBmp = new Bitmap(sourceImage.Width, sourceImage.Height))
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        Color pxl = bmp.GetPixel(x, y);

                        colors.Add(pxl);

                        // this is done twice and is lazy
                        int greyScaleColour = (pxl.R + pxl.G + pxl.B) / 3;
                        imageContainer.min = Math.Min(imageContainer.min, greyScaleColour);
                        imageContainer.max = Math.Max(imageContainer.max, greyScaleColour);
                    }
                }
            }


            imageContainer.colours = colors;
            return imageContainer;
        }

    }
}
