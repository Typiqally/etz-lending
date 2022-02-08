using AutoMapper;
using ETZ.Lending.Common.Extensions;
using ETZ.Lending.Domain.Abstractions.Models;
using ETZ.Lending.Presentation.WebApi.ViewModels;

namespace ETZ.Lending.Presentation.WebApi.Profiles
{
    public class FileViewModelMappingProfile : Profile
    {
        public FileViewModelMappingProfile()
        {
            CreateMap<FileViewModel, File>()
                .IgnoreMember(static member => member.Path)
                .IgnoreMember(static member => member.Stream);

            CreateMap<File, FileViewModel>();
        }
    }
}