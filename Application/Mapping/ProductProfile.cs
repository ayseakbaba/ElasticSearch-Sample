using Application.Dtos;
using Application.Models;
using AutoMapper;

namespace Application.Mapping
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
