using System;
using System.Windows.Forms;

namespace COMP1019_Buoi11_Lab11
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmLop());
        }
    }
}
