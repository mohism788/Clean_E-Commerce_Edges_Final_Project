using Clean_E_Commerce_Project.Core.Models;

namespace Clean_E_Commerce_Project.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        //get product by product id with username and category name

        Task<Product> GetProductWithDetailsByIdAsync(int id);
        Task<ICollection<Product>> GetAllProducts(string? filterBy, string? filterQuery, string? sortBy, bool? isAscending, int pageSize, int pageNumber);
    }
}
