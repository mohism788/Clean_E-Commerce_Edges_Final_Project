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

        //    // Configure one-to-many between Category and Products
        //    modelBuilder.Entity<Product>()
        //.HasOne<Category>()
        //.WithMany(c => c.Products)
        //.HasForeignKey(p => p.CategoryId);



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


        //    // Product -> Seller (Many-to-One)
        //    modelBuilder.Entity<Product>()
        //.HasOne(p => p.Seller)
        //.WithMany() // no nav property in ApplicationUser
        //.HasForeignKey(p => p.SellerId)
        //.OnDelete(DeleteBehavior.Restrict); // Prevent deleting Seller if they have Products


        //    // Order -> User (Many-to-One)
        //    modelBuilder.Entity<Order>()
        //.HasOne(o => o.User)
        //.WithMany() // no nav property in ApplicationUser
        //.HasForeignKey(o => o.UserId)
        //.OnDelete(DeleteBehavior.Cascade);

        //      // CartItem -> User (Many-to-One)
        //      modelBuilder.Entity<CartItem>()
        //.HasOne(ci => ci.User)
        //.WithMany() // no nav property in ApplicationUser
        //.HasForeignKey(ci => ci.UserId)
        //.OnDelete(DeleteBehavior.Cascade); // delete cart items if user is deleted





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


    }
}
