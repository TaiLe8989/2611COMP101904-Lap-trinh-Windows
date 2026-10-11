namespace ShopManagerMini.Models
{
    /// <summary>
    /// Khách hàng của cửa hàng.
    /// </summary>
    public class Customer
    {
        public string Id { get; set; }          // Mã khách hàng
        public string FullName { get; set; }    // Họ tên
        public string Phone { get; set; }       // Số điện thoại
        public string CustomerType { get; set; } // Loại khách hàng

        public Customer() { }

        public Customer(string id, string fullName, string phone, string customerType)
        {
            Id = id;
            FullName = fullName;
            Phone = phone;
            CustomerType = customerType;
        }
    }
}
