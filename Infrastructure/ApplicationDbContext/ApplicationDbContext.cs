using Clean_E_Commerce_Project.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Clean_E_Commerce_Project.Infrastructure.ApplicationDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configuration can go here
            //make a join many to many for category and product
            modelBuilder.Entity<Product>()
                .HasMany(p=> p.Categories)
                .WithMany(c => c.Products)
                .UsingEntity(j => j.ToTable("ProductsCategory"));


        }

        // Define DbSets for your entities
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<CartItem> CartItems { get; set; }


    }
}
