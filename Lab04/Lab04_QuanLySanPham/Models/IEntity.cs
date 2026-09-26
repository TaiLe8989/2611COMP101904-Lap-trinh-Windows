namespace Lab04_QuanLySanPham.Models
{
    
    /// Interface đánh dấu một đối tượng có định danh (Id).
    /// Dùng làm ràng buộc generic: Repository&lt;T&gt; where T : IEntity.
   
    public interface IEntity
    {
        string Id { get; }
    }
}
