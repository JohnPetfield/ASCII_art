using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ASCII_art_project
{
    public class ImageContainer
    {
        // this used by the mapping function to try ensure a full use of characters
        // even if the image is really dark or light
        public int min = 255, max = 0;

        public List<Color> colours;

        public ImageContainer()
        {
            colours = new();
        }

    }
}
