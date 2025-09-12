using AutoMapper;
using Clean_E_Commerce_Project.API.DTOs.ProductsDTOs;
using Clean_E_Commerce_Project.Core.Interfaces;
using Clean_E_Commerce_Project.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clean_E_Commerce_Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _unitOfWork.ProductsRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [HttpGet("/ProductDetails:{id}")]
        public async Task<IActionResult> GetProductDetailsById(int id)
        {
            var product = await _unitOfWork.ProductsRepository.GetProductWithDetailsByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto newProduct)
        {
            if (newProduct == null)
            {
                return BadRequest();
            }
            var product = _mapper.Map<Product>(newProduct);
            await _unitOfWork.ProductsRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto updataedProduct)
        {
            if (updataedProduct == null)
            {
                return BadRequest();
            }
            var existingProduct = await _unitOfWork.ProductsRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            var product = _mapper.Map<Product>(updataedProduct);
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            _unitOfWork.ProductsRepository.Update(existingProduct);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.ProductsRepository.Delete(product);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }




    }
}
