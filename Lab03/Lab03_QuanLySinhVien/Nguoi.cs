using System;

namespace Lab03_QuanLySinhVienOOP
{
   
    /// Lớp cha mô tả một con người: có họ tên và ngày sinh.
    /// SinhVien sẽ kế thừa từ lớp này.
   
    public class Nguoi
    {
        private string _hoTen = string.Empty;
        private DateTime _ngaySinh;

        /// Họ tên 
        public string HoTen
        {
            get => _hoTen;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Họ tên không được để trống.");
                }
                _hoTen = value.Trim();
            }
        }

        /// Ngày sinh (không được lớn hơn ngày hiện tại và không nhỏ hơn năm 1900).
        public DateTime NgaySinh
        {
            get => _ngaySinh;
            set
            {
                if (value.Date > DateTime.Today)
                {
                    throw new ArgumentException("Ngày sinh không được lớn hơn ngày hiện tại.");
                }
                if (value.Year < 1900)
                {
                    throw new ArgumentException("Ngày sinh không hợp lệ (năm phải từ 1900 trở đi).");
                }
                _ngaySinh = value.Date;
            }
        }

        /// Constructor của lớp cha, gán giá trị thông qua property để được kiểm tra dữ liệu.
        public Nguoi(string hoTen, DateTime ngaySinh)
        {
            HoTen = hoTen;
            NgaySinh = ngaySinh;
        }

        
        /// Trả về chuỗi thông tin của một người.
        /// Khai báo virtual để lớp con (SinhVien) có thể override.
        
        public virtual string LayThongTin()
        {
            return $"Họ tên: {HoTen} | Ngày sinh: {NgaySinh:dd/MM/yyyy}";
        }
    }
}
