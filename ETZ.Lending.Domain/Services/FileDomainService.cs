using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ETZ.Lending.Domain.Abstractions.Models;
using ETZ.Lending.Domain.Abstractions.Services;
using ETZ.Lending.Persistence.Abstractions.Entities;
using ETZ.Lending.Persistence.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace ETZ.Lending.Domain.Services
{
    public class FileDomainService : IFileDomainService
    {
        private readonly IMapper _mapper;
        private readonly IFileSystemRepository _fileSystemRepository;
        private readonly IFileManifestRepository _fileManifestRepository;

        public FileDomainService(
            IMapper mapper,
            IFileSystemRepository fileSystemRepository,
            IFileManifestRepository fileManifestRepository
        )
        {
            _mapper = mapper;
            _fileSystemRepository = fileSystemRepository;
            _fileManifestRepository = fileManifestRepository;
        }

        public async Task<IEnumerable<File>> GetAllAsync()
        {
            var storedFiles = await _fileManifestRepository.AllToListAsync();

            return _mapper.Map<IEnumerable<File>>(storedFiles);
        }

        public async Task<File> GetByIdAsync(Guid id, bool includeContents = false)
        {
            var manifestEntry = await _fileManifestRepository.FindAsync(id);
            if (manifestEntry == null) return null;

            var file = _mapper.Map<File>(manifestEntry);
            if (!includeContents) return file;

            var fileSystemEntry = await _fileSystemRepository.FindAsync(manifestEntry.Path);
            if (fileSystemEntry == null) return null;

            file.Stream = fileSystemEntry.Stream;

            return file;
        }

        public async Task<File> UploadAsync(IFormFile formFile)
        {
            var fileSystemEntity = new FileSystemEntity {Stream = formFile.OpenReadStream()};
            var createdFileSystemEntity = await _fileSystemRepository.CreateAsync(fileSystemEntity);

            var manifestEntity = _mapper.Map<FileManifestEntity>(formFile);
            manifestEntity.Path = createdFileSystemEntity.Path;
            
            var manifestEntry = await _fileManifestRepository.CreateAsync(manifestEntity);
            var affected = await _fileSystemRepository.SaveChangesAsync();
            
            if (affected != 1)
            {
                return null;
            }

            await _fileManifestRepository.SaveChangesAsync();
            
            var file = _mapper.Map<File>(manifestEntry);
            file.Stream = createdFileSystemEntity.Stream;

            return file;
        }

        public async Task<bool> ValidateFileAsync(Guid id, IEnumerable<string> contentTypes)
        {
            var file = await _fileManifestRepository.FindAsync(id);
            return file != null && contentTypes.Contains(file.ContentType);
        }
    }
}