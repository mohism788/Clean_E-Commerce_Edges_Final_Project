using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;

namespace Clean_E_Commerce_Project.Core.Models
{
    [NotMapped]
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        
    }
}
