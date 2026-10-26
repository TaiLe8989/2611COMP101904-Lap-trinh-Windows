using System;

namespace QuanLySuKienVaBanVe.DTO
{
    /// <summary>Đối tượng dữ liệu tài khoản; không đưa mật khẩu ra giao diện.</summary>
    public class TaiKhoanDTO
    {
        public int MaTK { get; set; }
        public string TenDangNhap { get; set; }
        // Mật khẩu hash chỉ được dùng giữa DAL/BLL, tuyệt đối không hiển thị ra GUI.
        public string MatKhau { get; set; }
        public string VaiTro { get; set; }
        public bool TrangThai { get; set; }

        public string TenVaiTro
        {
            get
            {
                if (string.Equals(VaiTro, "QuanLy", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(VaiTro, "Admin", StringComparison.OrdinalIgnoreCase))
                    return "Quản lý";
                return "Nhân viên";
            }
        }
    }
}
