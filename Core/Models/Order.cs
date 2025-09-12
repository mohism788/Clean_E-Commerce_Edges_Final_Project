using Microsoft.AspNetCore.Identity;

namespace Clean_E_Commerce_Project.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // e.g., Pending, Shipped, Delivered
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
