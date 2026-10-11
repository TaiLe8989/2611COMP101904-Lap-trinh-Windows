using System;
using System.Windows.Forms;
using ShopManagerMini.Forms;

namespace ShopManagerMini
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Hiển thị FrmLogin dạng hộp thoại; chỉ mở FrmMain khi đăng nhập thành công
            using (FrmLogin login = new FrmLogin())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new FrmMain(login.CurrentUser));
                }
            }
        }
    }
}
