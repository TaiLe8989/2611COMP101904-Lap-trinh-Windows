namespace Lab04_QuanLySanPham.Exceptions
{
    /// <summary>
    /// Phát sinh khi xóa (hoặc sửa) một sản phẩm không tồn tại trong kho.
    /// </summary>
    public class ProductNotFoundException : Exception
    {
        public string MaSP { get; }

        public ProductNotFoundException(string maSP)
            : base($"Không tìm thấy sản phẩm có mã '{maSP}'.")
        {
            MaSP = maSP;
        }
    }
}
