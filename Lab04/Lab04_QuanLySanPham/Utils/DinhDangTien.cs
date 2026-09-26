using System.Globalization;

namespace Lab04_QuanLySanPham.Utils
{
    
    /// Định dạng số tiền theo kiểu Việt Nam (ví dụ: 15.000 VNĐ).
    
    public static class DinhDangTien
    {
        private static readonly CultureInfo ViVn = new CultureInfo("vi-VN");

        public static string SangVnd(decimal soTien)
        {
            return soTien.ToString("N0", ViVn) + " VNĐ";
        }
    }
}
