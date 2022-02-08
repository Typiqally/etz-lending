using AutoMapper;
using ETZ.Lending.Domain.Abstractions.Models;
using ETZ.Lending.Persistence.Abstractions.Entities;

namespace ETZ.Lending.Domain.Profiles
{
    public class LentProductMappingProfile : Profile
    {
        public LentProductMappingProfile()
        {
            CreateMap<LentProduct, LentProductEntity>();
            CreateMap<LentProductEntity, LentProduct>();
        }
    }
}