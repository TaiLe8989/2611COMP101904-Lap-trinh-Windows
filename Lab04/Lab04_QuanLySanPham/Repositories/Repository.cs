using Lab04_QuanLySanPham.Models;

namespace Lab04_QuanLySanPham.Repositories
{
    ///  
    /// Kho lưu trữ generic trong bộ nhớ cho mọi kiểu T có Id (IEntity).
    /// Repository chỉ lo lưu trữ/truy vấn; các quy tắc nghiệp vụ nằm ở Service.
    ///  
    public class Repository<T> where T : IEntity
    {
        private readonly List<T> _items = new List<T>();

        public int Count => _items.Count;

        public void Add(T item)
        {
            ArgumentNullException.ThrowIfNull(item);
            _items.Add(item);
        }

        ///  Xóa phần tử theo Id. Trả về true nếu xóa được, false nếu không có. 
        public bool Remove(string id)
        {
            T? item = FindById(id);
            if (item == null)
            {
                return false;
            }
            return _items.Remove(item);
        }

        ///  Tìm theo Id (không phân biệt hoa/thường). Không thấy thì trả về null. 
        public T? FindById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return default;
            }

            string idCanTim = id.Trim();
            return _items.FirstOrDefault(
                item => string.Equals(item.Id, idCanTim, StringComparison.OrdinalIgnoreCase));
        }

        ///  Tìm các phần tử thỏa điều kiện do nơi gọi truyền vào (Func). 
        public List<T> Find(Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return _items.Where(predicate).ToList();
        }

        ///  Lấy toàn bộ phần tử (bản chỉ đọc, không cho sửa trực tiếp danh sách gốc). 
        public IReadOnlyList<T> GetAll()
        {
            return _items.AsReadOnly();
        }
    }
}
