namespace Clean_E_Commerce_Project.API.DTOs.ProductsDTOs
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set;}
        public int Stock { get; set; }
        public string SellerId { get; set; }
    }
}
