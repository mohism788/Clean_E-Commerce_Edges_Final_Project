using AutoMapper;
using Clean_E_Commerce_Project.API.DTOs.CategoriesDTOs;
using Clean_E_Commerce_Project.Core.Interfaces;
using Clean_E_Commerce_Project.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean_E_Commerce_Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            return Ok(categories);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto newCategory)
        {
            if (newCategory == null)
            {
                return BadRequest();
            }
            var category = _mapper.Map<Category>(newCategory);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdatedCategoryDto updatedCategory)
        {
            if (updatedCategory == null)
            {
                return BadRequest();
            }
            var existingCategory = await _unitOfWork.Categories.GetByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            _mapper.Map(updatedCategory, existingCategory);
            _unitOfWork.Categories.Update(existingCategory);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var existingCategory = await _unitOfWork.Categories.GetByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            _unitOfWork.Categories.Delete(existingCategory);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
