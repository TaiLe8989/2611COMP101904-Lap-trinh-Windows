using System.Collections.Generic;
using System.Drawing;

namespace COMP1019_Lab08_SimplePaint
{
    /// <summary>
    /// Dữ liệu gốc của một nét vẽ. Dựa vào danh sách DrawItem, chương trình
    /// có thể vẽ lại toàn bộ canvas bất cứ lúc nào (resize, bị che rồi hiện lại...).
    /// </summary>
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
