using System.Drawing;
using System.Windows.Forms;

namespace COMP1019_Lab08_SimplePaint
{
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
