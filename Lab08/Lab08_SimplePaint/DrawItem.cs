using System.Collections.Generic;
using System.Drawing;

namespace COMP1019_Lab08_SimplePaint
{
 
    public class DrawItem
    {
        public DrawMode Mode { get; set; }
        public Point Start { get; set; }
        public Point End { get; set; }
        public Color Color { get; set; }
        public float Width { get; set; }
        public List<Point> Points { get; set; } = new List<Point>();
    }
}
