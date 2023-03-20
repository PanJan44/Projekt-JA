using System;
using System.Collections.Generic;
using System.Text;

namespace JaProj
{
    public class HistogramValues
    {
        public int[] red = new int[256];
        public int[] green = new int[256];
        public int[] blue = new int[256];

        public HistogramValues(int[] red, int[] green, int[] blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }
    }
}
