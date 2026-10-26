namespace ShopManagerMini.Models
{
 
    public class Customer
    {
        public string Id { get; set; }          
        public string FullName { get; set; }    
        public string Phone { get; set; }       
        public string CustomerType { get; set; } 

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
