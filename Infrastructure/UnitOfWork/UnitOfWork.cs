using Clean_E_Commerce_Project.Core.Interfaces;
using Clean_E_Commerce_Project.Infrastructure.DataAccess;
using Clean_E_Commerce_Project.Infrastructure.Repositories;

namespace Clean_E_Commerce_Project.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly AuthDbContext _authDbContext;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReviewRepository _reviewRepository;
        public UnitOfWork(ApplicationDbContext dbContext, AuthDbContext authDbContext) 
        {
            _dbContext = dbContext;
            _authDbContext = authDbContext;
        }
        public IProductRepository ProductsRepository => _productRepository ?? new ProductRepository(_dbContext, _authDbContext);
        public ICategoryRepository Categories => _categoryRepository ?? new CategoryRepository(_dbContext);
        public IReviewRepository ReviewsRepository => _reviewRepository ?? new ReviewRepository(_dbContext);
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
