using Lab04_QuanLySanPham.Exceptions;
using Lab04_QuanLySanPham.Models;
using Lab04_QuanLySanPham.Repositories;

namespace Lab04_QuanLySanPham.Services
{
   
    /// Xử lý nghiệp vụ quản lý sản phẩm: kiểm tra dữ liệu, gọi Repository, phát event.

    public class ProductService
    {
        private readonly Repository<Product> _repository = new Repository<Product>();

        ///  Phát ra sau khi thêm sản phẩm thành công. 
        public event Action<Product>? ProductAdded;

        ///  Phát ra sau khi xóa sản phẩm thành công. 
        public event Action<Product>? ProductRemoved;

        public void AddProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            if (_repository.FindById(product.Id) != null)
            {
                throw new DuplicateProductException(product.MaSP);
            }

            _repository.Add(product);
            ProductAdded?.Invoke(product);
        }

        public void RemoveProduct(string maSP)
        {
            Product? product = _repository.FindById(maSP);
            if (product == null)
            {
                throw new ProductNotFoundException(maSP?.Trim() ?? string.Empty);
            }

            _repository.Remove(product.Id);
            ProductRemoved?.Invoke(product);
        }

        public IReadOnlyList<Product> GetAll()
        {
            return _repository.GetAll();
        }

        ///  Tìm theo mã. Trả về null nếu không có. 
        public Product? SearchById(string maSP)
        {
            return _repository.FindById(maSP);
        }

        ///  Tìm các sản phẩm có tên chứa từ khóa (không phân biệt hoa/thường). 
        public List<Product> SearchByName(string keyword)
        {
            string tuKhoa = (keyword ?? string.Empty).Trim();
            Func<Product, bool> tenChuaTuKhoa =
                p => p.TenSP.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase);

            return Filter(tenChuaTuKhoa);
        }

        ///  Lọc sản phẩm theo điều kiện bất kỳ dạng Func&lt;Product, bool&gt;. 
        public List<Product> Filter(Func<Product, bool> condition)
        {
            return _repository.Find(condition);
        }

        ///  Lọc sản phẩm có đơn giá trong khoảng [minPrice, maxPrice]. 
        public List<Product> FilterByPriceRange(decimal minPrice, decimal maxPrice)
        {
            if (minPrice < 0 || maxPrice < 0)
            {
                throw new ArgumentException("Khoảng giá không được âm.");
            }
            if (minPrice > maxPrice)
            {
                throw new ArgumentException("Giá nhỏ nhất không được lớn hơn giá lớn nhất.");
            }

            Func<Product, bool> giaTrongKhoang =
                p => p.Price >= minPrice && p.Price <= maxPrice;

            return Filter(giaTrongKhoang);
        }

        ///  Tổng giá trị kho = tổng (đơn giá * số lượng) của mọi sản phẩm. 
        public decimal CalculateTotalInventoryValue()
        {
            return _repository.GetAll().Sum(p => p.ThanhTien);
        }
    }
}
