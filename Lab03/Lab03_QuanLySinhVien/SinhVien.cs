using System;

namespace Lab03_QuanLySinhVienOOP
{
    
    /// Sinh viên kế thừa từ Nguoi: có thêm mã sinh viên, mã lớp và điểm trung bình.
    
    public class SinhVien : Nguoi
    {
        ///  Điểm tối thiểu để được xem là "đạt". 
        public const double DiemDat = 5.0;

        private string _maSinhVien = string.Empty;
        private string _maLop = string.Empty;
        private double _diemTrungBinh;

        ///  Mã sinh viên 
        public string MaSinhVien
        {
            get => _maSinhVien;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Mã sinh viên không được để trống.");
                }
                _maSinhVien = value.Trim();
            }
        }

        ///  Mã lớp 
        public string MaLop
        {
            get => _maLop;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Mã lớp không được để trống.");
                }
                _maLop = value.Trim();
            }
        }

        ///  Điểm trung bình, chỉ nhận giá trị trong khoảng 0 - 10. 
        public double DiemTrungBinh
        {
            get => _diemTrungBinh;
            set
            {
                if (!LaDiemHopLe(value))
                {
                    throw new ArgumentException("Điểm trung bình phải nằm trong khoảng từ 0 đến 10.");
                }
                _diemTrungBinh = value;
            }
        }

        ///  Constructor của SinhVien, gọi constructor lớp cha bằng base(...). 
        public SinhVien(string maSinhVien, string hoTen, DateTime ngaySinh, string maLop, double diemTrungBinh)
            : base(hoTen, ngaySinh)
        {
            MaSinhVien = maSinhVien;
            MaLop = maLop;
            DiemTrungBinh = diemTrungBinh;
        }

        ///  Kiểm tra điểm có nằm trong khoảng 0 - 10 (NaN hoặc vô cực cũng không hợp lệ). 
        public static bool LaDiemHopLe(double diem)
        {
            return diem >= 0 && diem <= 10;
        }

        ///  Xếp loại học lực theo điểm trung bình. 
        public string XepLoai()
        {
            if (DiemTrungBinh >= 9) return "Xuất sắc";
            if (DiemTrungBinh >= 8) return "Giỏi";
            if (DiemTrungBinh >= 7) return "Khá";
            if (DiemTrungBinh >= DiemDat) return "Trung bình";
            return "Yếu";
        }

        ///  Override: bổ sung thông tin sinh viên vào thông tin của lớp cha. 
        public override string LayThongTin()
        {
            return $"Mã SV: {MaSinhVien} | {base.LayThongTin()} | Lớp: {MaLop} | Điểm TB: {DiemTrungBinh:0.00} | Xếp loại: {XepLoai()}";
        }
    }
}
