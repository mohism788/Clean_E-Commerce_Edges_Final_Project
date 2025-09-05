using Clean_E_Commerce_Project.Core.Interfaces;
using Clean_E_Commerce_Project.Core.Models;
using Clean_E_Commerce_Project.Infrastructure.DataAccess;
using Clean_E_Commerce_Project.Infrastructure.Repositories.GenericRepo;

namespace Clean_E_Commerce_Project.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }


    }
}
