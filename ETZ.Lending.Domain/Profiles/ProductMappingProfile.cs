using AutoMapper;
using ETZ.Lending.Domain.Abstractions.Models;
using ETZ.Lending.Persistence.Abstractions.Entities;

namespace ETZ.Lending.Domain.Profiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductEntity>();
            CreateMap<ProductEntity, Product>();
        }
    }
}