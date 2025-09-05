using System.ComponentModel.DataAnnotations;

namespace Clean_E_Commerce_Project.API.DTOs.ReviewsDTOs
{
    public class UpdatedReviewDto

    {
        public int ProductId { get; set; }
        public string UserId { get; set; }


        [Range(1, 5)]
        public int Rating { get; set; } // e.g., 1 to 5


        public string Comment { get; set; }
    }
}
