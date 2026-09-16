using System;

namespace QuanLyNhanVien
{
   
    /// BONUS: Nhân viên thời vụ, kế thừa NhanVien.
    /// Lương = Số giờ làm x Lương theo giờ (không phụ thuộc Lương cơ bản).
    /// Nhờ đa hình, khi thêm lớp này KHÔNG cần sửa thuật toán tìm lương cao nhất
    /// hay tính tổng lương công ty ở Program.cs.
  
    public class NhanVienThoiVu : NhanVien
    {
        private int _soGioLam;
        public int SoGioLam
        {
            get => _soGioLam;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Số giờ làm phải >= 0.");
                _soGioLam = value;
            }
        }

        private double _luongTheoGio;
        public double LuongTheoGio
        {
            get => _luongTheoGio;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Lương theo giờ phải > 0.");
                _luongTheoGio = value;
            }
        }

        // Lương cơ bản của lớp cha chỉ mang tính chất khai báo bắt buộc (>0),
        // không tham gia công thức tính lương của nhân viên thời vụ.
        public NhanVienThoiVu(string maNV, string hoTen, double luongCoBan, int soGioLam, double luongTheoGio)
            : base(maNV, hoTen, luongCoBan)
        {
            SoGioLam = soGioLam;
            LuongTheoGio = luongTheoGio;
        }

        public override double TinhLuong()
        {
            return LuongCoBan + SoGioLam * LuongTheoGio;
        }

        public override void HienThiThongTin()
        {
            Console.WriteLine(
                $"{MaNV,-8} | {HoTen,-22} | {"NV Thời vụ",-20} | {TinhLuong(),15:N0} VNĐ | Số giờ: {SoGioLam} x {LuongTheoGio:N0}/giờ");
        }
    }
}
