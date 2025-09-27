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

        public async Task<ICollection<Product>> GetAllProducts(string? filterBy, string? filterQuery,
                                                         string? sortBy, bool? isAscending,
                                                           int pageSize = 30, int pageNumber =1)
        {

            var query = _dbContext.Products.AsQueryable();
            // Filtering
            if (!string.IsNullOrEmpty(filterBy) && !string.IsNullOrEmpty(filterQuery))
            {
                switch (filterBy.ToLower())
                {
                    case "name":
                        query = query.Where(p => p.Name.Contains(filterQuery));
                        break;
                    case "category":
                        query = query.Where(p => p.Category.Name.Contains(filterQuery));
                        break;
                    case "seller":
                        query = query.Where(p => p.SellerId.Contains(filterQuery));
                        break;
                        // Add more filters as needed
                }
            }
            // Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        query = isAscending == true ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
                        break;
                    case "price":
                        query = isAscending == true ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
                        break;
                    case "createdat":
                        query = isAscending == true ? query.OrderBy(p => p.CreatedAt) : query.OrderByDescending(p => p.CreatedAt);
                        break;
                        // Add more sorting options as needed
                }
            }
            // Pagination
            var skip = (pageNumber - 1) * pageSize;
            query = query.Skip(skip).Take(pageSize);
            return await query.ToListAsync();

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
