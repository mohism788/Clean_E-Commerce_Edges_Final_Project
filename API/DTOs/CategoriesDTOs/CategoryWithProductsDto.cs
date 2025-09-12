using Clean_E_Commerce_Project.API.DTOs.ProductsDTOs;
using Clean_E_Commerce_Project.Core.Models;

namespace Clean_E_Commerce_Project.API.DTOs.CategoriesDTOs
{
    public class CategoryWithProductsDto
    {
        public string Name { get; set; }
        public ICollection<ProductWithoutCategoryDto> Products { get; set; }
    }
}
