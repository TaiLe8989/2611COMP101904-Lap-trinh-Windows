using System.Globalization;
using QLSinhVienLab12.Forms;

namespace QLSinhVienLab12
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            ApplicationConfiguration.Initialize();
            Application.Run(new FrmNhapDiemTheoLop());
        }
    }
}
