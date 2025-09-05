using AutoMapper;
using Clean_E_Commerce_Project.API.DTOs.CategoriesDTOs;
using Clean_E_Commerce_Project.API.DTOs.ProductsDTOs;
using Clean_E_Commerce_Project.API.DTOs.ReviewsDTOs;
using Clean_E_Commerce_Project.Core.Models;

namespace Clean_E_Commerce_Project.Core.MapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            // CreateMap<Source, Destination>();
            // Example:
            // CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>().ReverseMap();
            CreateMap<UpdateProductDto, Product>().ReverseMap();
            CreateMap<UpdatedCategoryDto, Category>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>().ReverseMap();
            CreateMap<CreateReviewDto, Review>().ReverseMap();
            CreateMap<UpdatedReviewDto, Review>().ReverseMap();


        }

    }
}
