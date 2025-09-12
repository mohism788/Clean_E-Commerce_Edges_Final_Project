using Clean_E_Commerce_Project.Core.Interfaces;
using Clean_E_Commerce_Project.Core.Models;
using Clean_E_Commerce_Project.Infrastructure.DataAccess;
using Clean_E_Commerce_Project.Infrastructure.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;

namespace Clean_E_Commerce_Project.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly AuthDbContext _authDbContext;

        public ProductRepository(ApplicationDbContext dbContext, AuthDbContext authDbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _authDbContext = authDbContext;
        }

        public async Task<Product> GetProductWithDetailsByIdAsync(int id)
        {
            var product = await _dbContext.Products
                .Where(p => p.Id == id)
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    CreatedAt = p.CreatedAt,
                    CategoryId = p.CategoryId,
                    Category = p.Category,
                    SellerId = p.SellerId,
                    Reviews = p.Reviews,
                    OrderItems = p.OrderItems
                })
                .FirstOrDefaultAsync();
            if (product == null)
            {
                throw new Exception($"Product with id {id} not found.");
            }
            var user = await _authDbContext.Users.FindAsync(product.SellerId);
            if (user != null)
            {
                // Assuming you want to include the username in the product details
                product.SellerId = user.UserName; // Replace SellerId with Seller's Username for display purposes
            }
            return product;
        }
    }
}
