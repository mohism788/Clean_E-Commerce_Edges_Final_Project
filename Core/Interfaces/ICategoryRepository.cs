using Clean_E_Commerce_Project.Core.Models;

namespace Clean_E_Commerce_Project.Core.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetCategoryWithProductsByIdAsync(int id);

    }
}
