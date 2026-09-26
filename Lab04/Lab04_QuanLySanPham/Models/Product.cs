using Lab04_QuanLySanPham.Utils;

namespace Lab04_QuanLySanPham.Models
{
    /// 
    /// Sản phẩm trong kho. Dữ liệu luôn được kiểm tra hợp lệ ngay khi gán:
    /// mã và tên không rỗng, đơn giá và số lượng không âm.
    /// 
    public class Product : IEntity
    {
        private string _tenSP = string.Empty;
        private decimal _price;
        private int _quantity;

        public string MaSP { get; }

        public string TenSP
        {
            get => _tenSP;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Tên sản phẩm không được để trống.");
                }
                _tenSP = value.Trim();
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Đơn giá không được âm.");
                }
                _price = value;
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Số lượng không được âm.");
                }
                _quantity = value;
            }
        }

        // Id chính là mã sản phẩm, dùng cho ràng buộc generic của Repository<T>.
        public string Id => MaSP;

        /// Thành tiền của sản phẩm = đơn giá * số lượng.</summary>
        public decimal ThanhTien => Price * Quantity;

        public Product(string maSP, string tenSP, decimal price, int quantity)
        {
            if (string.IsNullOrWhiteSpace(maSP))
            {
                throw new ArgumentException("Mã sản phẩm không được để trống.");
            }

            MaSP = maSP.Trim();
            TenSP = tenSP;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"Mã: {MaSP,-8} | Tên: {TenSP,-22} | Đơn giá: {DinhDangTien.SangVnd(Price),15} | Số lượng: {Quantity,5}";
        }
    }
}
