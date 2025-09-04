using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;

namespace Clean_E_Commerce_Project.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string[] Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Product> Products{ get; set; }
    }
}
