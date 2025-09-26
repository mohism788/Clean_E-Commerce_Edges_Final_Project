namespace Clean_E_Commerce_Project.Core.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public decimal TotalPrice => CartItems.Sum(item => item.Quantity * item.Product.Price); // Simplified
    }
}
