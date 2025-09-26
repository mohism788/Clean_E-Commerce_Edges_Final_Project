using Clean_E_Commerce_Project.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Clean_E_Commerce_Project.Infrastructure.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           

            modelBuilder.Entity<Cart>()
    .HasMany(c => c.CartItems)
    .WithOne(ci => ci.Cart)
    .HasForeignKey(ci => ci.CartId)
    .OnDelete(DeleteBehavior.Cascade); // Delete items if cart deleted

           

            // Configure one-to-many between Product and Reviews
            modelBuilder.Entity<Review>()
        .HasOne(r => r.Product)
        .WithMany(p => p.Reviews)
        .HasForeignKey(r => r.ProductId)
        .OnDelete(DeleteBehavior.Cascade); // delete reviews if product is deleted
   
             // CartItem -> Product (Many-to-One)
             modelBuilder.Entity<CartItem>()
        .HasOne(ci => ci.Product)
        .WithMany() // we don’t need a navigation property in Product unless you want `Product.CartItems`
        .HasForeignKey(ci => ci.ProductId)
        .OnDelete(DeleteBehavior.Cascade);

  
              // Order -> OrderItems (One-to-Many)
              modelBuilder.Entity<Order>()
        .HasMany(o => o.OrderItems)
        .WithOne(oi => oi.Order)
        .HasForeignKey(oi => oi.OrderId)
        .OnDelete(DeleteBehavior.Cascade);


              // OrderItem -> Product (Many-to-One)
              modelBuilder.Entity<OrderItem>()
        .HasOne(oi => oi.Product)
        .WithMany(p => p.OrderItems)
        .HasForeignKey(oi => oi.ProductId)
        .OnDelete(DeleteBehavior.Restrict); // Restrict so deleting a Product doesn’t nuke past Orders



            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
            modelBuilder.Entity<Review>().ToTable("Reviews");
            modelBuilder.Entity<CartItem>().ToTable("CartItems");


        }

        // Define DbSets for your entities
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts{ get; set; }


    }
}
