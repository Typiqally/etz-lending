using AutoMapper;
using ETZ.Lending.Domain.Abstractions.Models;
using ETZ.Lending.Presentation.WebApi.ViewModels;

namespace ETZ.Lending.Presentation.WebApi.Profiles
{
    public class LentProductViewModelMappingProfile : Profile
    {
        public LentProductViewModelMappingProfile()
        {
            CreateMap<LentProductViewModel, LentProduct>();
            CreateMap<LentProduct, LentProductViewModel>();
        }
    }
}