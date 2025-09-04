using System.ComponentModel.DataAnnotations;

namespace Clean_E_Commerce_Project.Core.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }


        [Range(1,5)]
        public int Rating { get; set; } // e.g., 1 to 5


        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}
