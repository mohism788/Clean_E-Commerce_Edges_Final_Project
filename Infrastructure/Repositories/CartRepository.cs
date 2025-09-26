using Clean_E_Commerce_Project.Core.Interfaces;
using Clean_E_Commerce_Project.Core.Models;
using Clean_E_Commerce_Project.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Clean_E_Commerce_Project.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Cart> GetByIdAsync(int id)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ToListAsync();
        }

        public async Task AddAsync(Cart entity)
        {
            await _context.Carts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cart entity)
        {
            _context.Carts.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cart = await GetByIdAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task AddOrUpdateItemAsync(int cartId, int productId, int quantity)
        {
            var cart = await GetByIdAsync(cartId) ?? throw new ArgumentException("Cart not found.");
            var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem { CartId = cartId, ProductId = productId, Quantity = quantity });
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(int cartId, int productId)
        {
            var cart = await GetByIdAsync(cartId) ?? throw new ArgumentException("Cart not found.");
            var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (item != null)
            {
                cart.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(int cartId)
        {
            var cart = await GetByIdAsync(cartId) ?? throw new ArgumentException("Cart not found.");
            cart.CartItems.Clear();
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetTotalPriceAsync(int cartId)
        {
            var cart = await GetByIdAsync(cartId) ?? throw new ArgumentException("Cart not found.");
            return cart.TotalPrice; // Uses the computed property
        }

        public void Update(Cart entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Cart entity)
        {
            throw new NotImplementedException();
        }
    }
}

