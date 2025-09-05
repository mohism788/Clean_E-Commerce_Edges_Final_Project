using Clean_E_Commerce_Project.Core.Interfaces;
using Clean_E_Commerce_Project.Infrastructure.DataAccess;
using Clean_E_Commerce_Project.Infrastructure.Repositories;

namespace Clean_E_Commerce_Project.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public UnitOfWork(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public IProductRepository ProductsRepository => _productRepository ?? new ProductRepository(_dbContext);
        public ICategoryRepository Categories => _categoryRepository ?? new CategoryRepository(_dbContext);
        public void Dispose()
        {
            
            _dbContext.Dispose();
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();

       }
    }
}
