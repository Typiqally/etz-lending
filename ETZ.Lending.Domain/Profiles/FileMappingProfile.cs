using AutoMapper;
using ETZ.Lending.Common.Extensions;
using ETZ.Lending.Domain.Abstractions.Models;
using ETZ.Lending.Persistence.Abstractions.Entities;
using Microsoft.AspNetCore.Http;

namespace ETZ.Lending.Domain.Profiles
{
    public class FileMappingProfile : Profile
    {
        public FileMappingProfile()
        {
            // File, FileManifest
            CreateMap<File, FileManifestEntity>();
            CreateMap<FileManifestEntity, File>()
                .IgnoreMember(static member => member.Stream);

            // IFormFile, FileManifestEntry
            CreateMap<IFormFile, FileManifestEntity>()
                .IgnoreMember(static member => member.Id)
                .IgnoreMember(static member => member.Path)
                .IgnoreMember(static member => member.CreatedAt)
                .IgnoreMember(static member => member.UpdatedAt);
        }
    }
}