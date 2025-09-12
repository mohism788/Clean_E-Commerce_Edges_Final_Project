using Clean_E_Commerce_Project.Core.Interfaces;
using Clean_E_Commerce_Project.Core.Models;
using Clean_E_Commerce_Project.Infrastructure.DataAccess;
using Clean_E_Commerce_Project.Infrastructure.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;

namespace Clean_E_Commerce_Project.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> GetCategoryWithProductsByIdAsync(int id)
        {
            var exist = await _dbContext.Categories.FindAsync(id);
            if (exist == null)
            {
                throw new Exception($"Category with id {id} not found.");
            }
            //return the category with its products
            var productsInCategory=  await _dbContext.Categories
                    .Where(c => c.Id == id)
                    .Select(c => new Category
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Products = c.Products.Select(p => new Product
                        {
                            Id = p.Id,
                            Name = p.Name,
                            SellerId = p.SellerId,
                            Price = p.Price,
                            Stock = p.Stock,
                            CreatedAt = p.CreatedAt
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

            if (productsInCategory == null)
            {
                throw new Exception($"Category with id {id} not found.");
            }

            return productsInCategory;

        }
    }
}
