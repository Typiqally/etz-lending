using ETZ.Lending.Persistence.Abstractions.Entities;
using ETZ.Lending.Persistence.Abstractions.Repositories;
using ETZ.Lending.Persistence.Contexts;

namespace ETZ.Lending.Persistence.Repositories
{
    public class FileManifestRepository : EntityFrameworkCoreRepository<FileManifestEntity>, IFileManifestRepository
    {
        public FileManifestRepository(IDbContext context) : base(context)
        {
        }
    }
}