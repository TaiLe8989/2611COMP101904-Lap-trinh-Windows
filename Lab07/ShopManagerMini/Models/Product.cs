namespace ShopManagerMini.Models
{
    /// <summary>
    /// Sản phẩm của cửa hàng.
    /// </summary>
    public class Product
    {
        public string Id { get; set; }          // Mã sản phẩm
        public string Name { get; set; }        // Tên sản phẩm
        public string Category { get; set; }    // Loại sản phẩm
        public decimal Price { get; set; }      // Đơn giá
        public int Quantity { get; set; }       // Số lượng

        public Product() { }

        public Product(string id, string name, string category, decimal price, int quantity)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
            Quantity = quantity;
        }
    }
}
