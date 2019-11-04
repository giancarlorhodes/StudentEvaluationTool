using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEvaluationToolCommon
{
    public class RandomColorGenerator
    {

        Random r = new Random(DateTime.Now.Millisecond);

        public RandomColorGenerator()
        {
        }

        private Color RandomColor()
        {
            byte red = (byte)r.Next(0, 255);
            byte green = (byte)r.Next(0, 255);
            byte blue = (byte)r.Next(0, 255);

            return Color.FromArgb(red, green, blue);
        }

        public string RGBAString()
        {

            string opacity = "0.5";
            Color c = this.RandomColor();
            return "rgba(" + Convert.ToUInt16(c.R) + "," + Convert.ToUInt16(c.G) + "," + Convert.ToUInt16(c.B) + "," + opacity + ")";
        }
    }
}
