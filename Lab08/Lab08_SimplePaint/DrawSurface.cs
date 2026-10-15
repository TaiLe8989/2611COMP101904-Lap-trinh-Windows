using System.Drawing;
using System.Windows.Forms;

namespace COMP1019_Lab08_SimplePaint
{
    /// <summary>Panel tùy chỉnh bật DoubleBuffered để vẽ không bị nhấp nháy.</summary>
    public class DrawSurface : Panel
    {
        public DrawSurface()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.White;
            Dock = DockStyle.Fill;
            Cursor = Cursors.Cross;
        }
    }
}
