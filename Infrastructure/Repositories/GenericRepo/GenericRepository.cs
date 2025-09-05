using Clean_E_Commerce_Project.Core.Interfaces;
using Clean_E_Commerce_Project.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Clean_E_Commerce_Project.Infrastructure.Repositories.GenericRepo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var exist =  await _dbContext.Set<T>().FindAsync(id);
            if (exist == null)
            {
                throw new Exception($"Entity of type {typeof(T).Name} with id {id} not found.");
            }
            return exist;
        }
        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }



    }
}
