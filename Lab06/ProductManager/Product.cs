using System;

namespace ProductManager
{
    /// <summary>
    /// Lớp lưu thông tin một sản phẩm (chỉ chứa dữ liệu, không xử lý giao diện).
    /// </summary>
    public class Product
    {
        public string MaSP { get; set; } = string.Empty;
        public string TenSP { get; set; } = string.Empty;
        public string LoaiSP { get; set; } = string.Empty;
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayNhap { get; set; }
        public bool ConKinhDoanh { get; set; }
        public string DuongDanAnh { get; set; } = string.Empty;

        // Thuộc tính tính toán: thành tiền = đơn giá x số lượng
        public decimal ThanhTien
        {
            get { return DonGia * SoLuong; }
        }
    }
}
