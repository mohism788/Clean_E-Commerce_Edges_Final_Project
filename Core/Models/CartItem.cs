using Microsoft.AspNetCore.Identity;

namespace Clean_E_Commerce_Project.Core.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; } // Add FK to Cart
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Cart Cart { get; set; } // Add navigation
    }
}
