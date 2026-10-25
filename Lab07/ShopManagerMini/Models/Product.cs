namespace ShopManagerMini.Models
{
   >
    public class Product
    {
        public string Id { get; set; }         
        public string Name { get; set; }       
        public string Category { get; set; }    
        public decimal Price { get; set; }      
        public int Quantity { get; set; }      

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
