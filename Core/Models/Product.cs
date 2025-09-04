using System.ComponentModel;

namespace Clean_E_Commerce_Project.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int SellerId{ get; set; }
        public DateTime CreatedAt{ get; set; } = DateTime.UtcNow;

        public ICollection<Category> Categories { get; set; }
        public ICollection<Review> Reviews{ get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
