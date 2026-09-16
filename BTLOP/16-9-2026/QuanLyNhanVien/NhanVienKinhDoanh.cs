using System;

namespace QuanLyNhanVien
{
   
    /// Nhân viên kinh doanh: kế thừa NhanVien, bổ sung Doanh số.
    /// Lương = Lương cơ bản + 5% x Doanh số
   
    public class NhanVienKinhDoanh : NhanVien
    {
        private const double TI_LE_HOA_HONG = 0.05;

        private double _doanhSo;
        public double DoanhSo
        {
            get => _doanhSo;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Doanh số phải >= 0.");
                _doanhSo = value;
            }
        }

        public NhanVienKinhDoanh(string maNV, string hoTen, double luongCoBan, double doanhSo)
            : base(maNV, hoTen, luongCoBan)
        {
            DoanhSo = doanhSo;
        }

        public override double TinhLuong()
        {
            return LuongCoBan + TI_LE_HOA_HONG * DoanhSo;
        }

        public override void HienThiThongTin()
        {
            Console.WriteLine(
                $"{MaNV,-8} | {HoTen,-22} | {"NV Kinh doanh",-20} | {TinhLuong(),15:N0} VNĐ | Doanh số: {DoanhSo:N0}");
        }
    }
}
