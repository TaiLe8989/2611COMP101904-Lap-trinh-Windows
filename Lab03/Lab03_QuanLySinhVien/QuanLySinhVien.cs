using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab03_QuanLySinhVienOOP
{
    
    /// Lớp xử lý nghiệp vụ (service): quản lý List&lt;SinhVien&gt;.
    /// Program (Main) chỉ gọi các phương thức ở đây, không thao tác trực tiếp trên danh sách.
    
    public class QuanLySinhVien
    {
        // Danh sách để private: bên ngoài không thể tự Add/Remove mà không qua các phương thức bên dưới.
        private readonly List<SinhVien> _danhSach = new List<SinhVien>();

        ///Số sinh viên hiện có.
        public int SoLuong => _danhSach.Count;

        /// Thêm sinh viên. Trả về false nếu mã sinh viên đã tồn tại.
        public bool Them(SinhVien sinhVien)
        {
            if (sinhVien == null)
            {
                throw new ArgumentNullException(nameof(sinhVien));
            }
            if (TonTaiMa(sinhVien.MaSinhVien))
            {
                return false;
            }
            _danhSach.Add(sinhVien);
            return true;
        }

        ///  Kiểm tra mã sinh viên đã tồn tại chưa (không phân biệt hoa thường). 
        public bool TonTaiMa(string maSinhVien)
        {
            return TimTheoMa(maSinhVien) != null;
        }

        ///  Tìm sinh viên theo mã (LINQ). Trả về null nếu không tìm thấy. 
        public SinhVien? TimTheoMa(string maSinhVien)
        {
            if (string.IsNullOrWhiteSpace(maSinhVien))
            {
                return null;
            }
            string ma = maSinhVien.Trim();
            return _danhSach.FirstOrDefault(sv =>
                string.Equals(sv.MaSinhVien, ma, StringComparison.OrdinalIgnoreCase));
        }

        ///  Tìm các sinh viên có họ tên chứa từ khóa (LINQ, không phân biệt hoa thường). 
        public List<SinhVien> TimTheoTen(string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                return new List<SinhVien>();
            }
            string khoa = tuKhoa.Trim();
            return _danhSach
                .Where(sv => sv.HoTen.Contains(khoa, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        ///  Sửa điểm trung bình. Trả về false nếu không tìm thấy mã sinh viên. 
        public bool SuaDiem(string maSinhVien, double diemMoi)
        {
            SinhVien? sinhVien = TimTheoMa(maSinhVien);
            if (sinhVien == null)
            {
                return false;
            }
            sinhVien.DiemTrungBinh = diemMoi; // setter tự kiểm tra 0 - 10
            return true;
        }

        ///  Xóa sinh viên theo mã. Trả về false nếu không tìm thấy. 
        public bool Xoa(string maSinhVien)
        {
            SinhVien? sinhVien = TimTheoMa(maSinhVien);
            if (sinhVien == null)
            {
                return false;
            }
            return _danhSach.Remove(sinhVien);
        }

        ///  Trả về danh sách mới đã sắp xếp theo điểm giảm dần (LINQ), không làm thay đổi danh sách gốc. 
        public List<SinhVien> SapXepTheoDiem()
        {
            return _danhSach
                .OrderByDescending(sv => sv.DiemTrungBinh)
                .ThenBy(sv => sv.HoTen)
                .ToList();
        }

        ///  Lọc các sinh viên đạt (điểm trung bình từ 5 trở lên) bằng LINQ. 
        public List<SinhVien> LocSinhVienDat()
        {
            return _danhSach
                .Where(sv => sv.DiemTrungBinh >= SinhVien.DiemDat)
                .ToList();
        }

        ///  Trả về danh sách chỉ đọc để bên ngoài xem/in, không sửa được danh sách bên trong. 
        public IReadOnlyList<SinhVien> LayDanhSach()
        {
            return _danhSach.AsReadOnly();
        }
    }
}
