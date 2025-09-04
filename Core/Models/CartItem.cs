namespace Clean_E_Commerce_Project.Core.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}
