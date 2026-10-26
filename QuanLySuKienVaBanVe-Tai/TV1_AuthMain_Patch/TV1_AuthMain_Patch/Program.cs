using System;
using System.Windows.Forms;
using QuanLySuKienVaBanVe.GUI.Forms;

namespace QuanLySuKienVaBanVe
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmDangNhap());
        }
    }
}
