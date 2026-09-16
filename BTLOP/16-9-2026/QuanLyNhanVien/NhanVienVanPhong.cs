using System;

namespace QuanLyNhanVien
{
   
    /// Nhân viên văn phòng: kế thừa NhanVien, bổ sung Số ngày làm việc.
    /// Lương = Lương cơ bản + Số ngày làm việc x 200.000
 
    public class NhanVienVanPhong : NhanVien
    {
        private const double PHU_CAP_MOI_NGAY = 200_000;

        private int _soNgayLamViec;
        public int SoNgayLamViec
        {
            get => _soNgayLamViec;
            set
            {
                if (value < 0 || value > 31)
                    throw new ArgumentException("Số ngày làm việc phải trong khoảng 0-31.");
                _soNgayLamViec = value;
            }
        }

        // CONSTRUCTOR dùng base(...) để tái sử dụng lớp cha 
        public NhanVienVanPhong(string maNV, string hoTen, double luongCoBan, int soNgayLamViec)
            : base(maNV, hoTen, luongCoBan)
        {
            SoNgayLamViec = soNgayLamViec;
        }

        // OVERRIDE: đa hình 
        public override double TinhLuong()
        {
            return LuongCoBan + SoNgayLamViec * PHU_CAP_MOI_NGAY;
        }

        public override void HienThiThongTin()
        {
            Console.WriteLine(
                $"{MaNV,-8} | {HoTen,-22} | {"NV Văn phòng",-20} | {TinhLuong(),15:N0} VNĐ | Số ngày làm: {SoNgayLamViec}");
        }
    }
}
