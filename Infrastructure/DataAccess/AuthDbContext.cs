using System.Reflection.Emit;
using Clean_E_Commerce_Project.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clean_E_Commerce_Project.Infrastructure.DataAccess
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            base.OnModelCreating(builder);

            var adminRoleId = "c8dd9907-521d-4dfc-8f91-fc85423eb5a1";
            var customerRoleId = "ce1167fb-70c1-4ff4-9095-6a8a44735e90";
            var sellerRoleId = "0fd352d3-5fd5-4072-8f6f-6d6db621c77a";


            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = customerRoleId,
                    Name = "customer",
                    NormalizedName = "customer".ToUpper()
                },
                new IdentityRole()
                {
                    Id = sellerRoleId,
                    Name = "Seller",
                    NormalizedName = "SELLER"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
