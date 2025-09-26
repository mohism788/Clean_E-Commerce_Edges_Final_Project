using Clean_E_Commerce_Project.Core.Models;

namespace Clean_E_Commerce_Project.Core.Interfaces
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart> GetCartByUserIdAsync(string userId);
        Task AddOrUpdateItemAsync(int cartId, int productId, int quantity);
        Task RemoveItemAsync(int cartId, int productId);
        Task ClearCartAsync(int cartId);
        Task<decimal> GetTotalPriceAsync(int cartId);

    }
}
