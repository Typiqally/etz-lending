using AutoMapper;
using ETZ.Lending.Domain.Abstractions.Models;
using ETZ.Lending.Presentation.WebApi.ViewModels;

namespace ETZ.Lending.Presentation.WebApi.Profiles
{
    public class ProductViewModelMappingProfile : Profile
    {
        public ProductViewModelMappingProfile()
        {
            CreateMap<ProductViewModel, Product>();
            CreateMap<Product, ProductViewModel>();
        }
    }
}