using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Clean_E_Commerce_Project.Core.Interfaces;
using Clean_E_Commerce_Project.Core.Models;

namespace Clean_E_Commerce_Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized("User not authenticated.");

            var cart = await _unitOfWork.CartsRepository.GetCartByUserIdAsync(userId);
            if (cart == null) return NotFound("Cart not found.");

            return Ok(cart);
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddItemToCart([FromBody] AddCartItemDto itemDto)
        {
            if (itemDto == null || itemDto.ProductId <= 0 || itemDto.Quantity <= 0)
                return BadRequest("Invalid item data.");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized("User not authenticated.");

            try
            {
                var cart = await _unitOfWork.CartsRepository.GetCartByUserIdAsync(userId);
                int cartId = cart?.Id ?? 0;
                if (cart == null)
                {
                    cart = new Cart { UserId = userId };
                    await _unitOfWork.CartsRepository.AddAsync(cart);
                    await _unitOfWork.SaveChangesAsync();
                }
                await _unitOfWork.CartsRepository.AddOrUpdateItemAsync(cartId, itemDto.ProductId, itemDto.Quantity);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Item added to cart successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding item: {ex.Message}");
            }
        }

        [HttpDelete("remove-item/{productId}")]
        public async Task<IActionResult> RemoveItemFromCart(int productId)
        {
            if (productId <= 0) return BadRequest("Invalid product ID.");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized("User not authenticated.");

            var cart = await _unitOfWork.CartsRepository.GetCartByUserIdAsync(userId);
            if (cart == null) return NotFound("Cart not found.");

            try
            {
                await _unitOfWork.CartsRepository.RemoveItemAsync(cart.Id, productId);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Item removed from cart successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error removing item: {ex.Message}");
            }
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized("User not authenticated.");

            var cart = await _unitOfWork.CartsRepository.GetCartByUserIdAsync(userId);
            if (cart == null) return NotFound("Cart not found.");

            try
            {
                await _unitOfWork.CartsRepository.ClearCartAsync(cart.Id);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Cart cleared successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error clearing cart: {ex.Message}");
            }
        }

        [HttpGet("total-price")]
        public async Task<IActionResult> GetTotalPrice()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized("User not authenticated.");

            var cart = await _unitOfWork.CartsRepository.GetCartByUserIdAsync(userId);
            if (cart == null) return NotFound("Cart not found.");

            try
            {
                var totalPrice = await _unitOfWork.CartsRepository.GetTotalPriceAsync(cart.Id);
                return Ok(new { TotalPrice = totalPrice });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calculating total price: {ex.Message}");
            }
        }
    }

    // DTO for adding items to cart
    public class AddCartItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
