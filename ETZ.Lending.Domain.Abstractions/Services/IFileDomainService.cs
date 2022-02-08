using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ETZ.Lending.Domain.Abstractions.Models;
using Microsoft.AspNetCore.Http;

namespace ETZ.Lending.Domain.Abstractions.Services
{
    public interface IFileDomainService
    {
        Task<IEnumerable<File>> GetAllAsync();
        Task<File> GetByIdAsync(Guid id, bool includeContents = false);
        Task<File> UploadAsync(IFormFile formFile);
        Task<bool> ValidateFileAsync(Guid id, IEnumerable<string> contentTypes);
    }
}