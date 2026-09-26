namespace Lab04_QuanLySanPham.Exceptions
{
   
    /// Phát sinh khi thêm sản phẩm có mã đã tồn tại trong kho.
   
    public class DuplicateProductException : Exception
    {
        public string MaSP { get; }

        public DuplicateProductException(string maSP)
            : base($"Mã sản phẩm '{maSP}' đã tồn tại, không thể thêm trùng.")
        {
            MaSP = maSP;
        }
    }
}
