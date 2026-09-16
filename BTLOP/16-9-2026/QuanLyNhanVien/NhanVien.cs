using System;

namespace QuanLyNhanVien
{

    /// Lớp cơ sở (base class) mô tả một nhân viên nói chung.
    /// Áp dụng: Class, Property, Constructor, Encapsulation.

    public class NhanVien
    {
        // Dùng field private + property để kiểm soát dữ liệu 
        private string _maNV = string.Empty;
        public string MaNV
        {
            get => _maNV;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Mã nhân viên không được để trống.");
                _maNV = value.Trim();
            }
        }

        private string _hoTen = string.Empty;
        public string HoTen
        {
            get => _hoTen;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Họ tên không được để trống.");
                _hoTen = value.Trim();
            }
        }

        private double _luongCoBan;
        public double LuongCoBan
        {
            get => _luongCoBan;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Lương cơ bản phải lớn hơn 0.");
                _luongCoBan = value;
            }
        }

        // CONSTRUCTOR: khởi tạo thông tin 
        public NhanVien(string maNV, string hoTen, double luongCoBan)
        {
            MaNV = maNV;
            HoTen = hoTen;
            LuongCoBan = luongCoBan;
        }

        // ĐA HÌNH: các phương thức virtual để lớp con override ----

        //Tính lương. Lớp cơ sở: lương = lương cơ bản.
        public virtual double TinhLuong()
        {
            return LuongCoBan;
        }

        //Hiển thị thông tin nhân viên (đa hình theo từng loại nhân viên).
        public virtual void HienThiThongTin()
        {
            Console.WriteLine(
                $"{MaNV,-8} | {HoTen,-22} | {"Nhân viên",-20} | {TinhLuong(),15:N0} VNĐ");
        }
    }
}
