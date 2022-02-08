using ETZ.Lending.Persistence.Abstractions.Entities;
using ETZ.Lending.Persistence.Abstractions.Repositories;
using ETZ.Lending.Persistence.Contexts;

namespace ETZ.Lending.Persistence.Repositories
{
    public class LentProductRepository : EntityFrameworkCoreRepository<LentProductEntity>, ILentProductRepository
    {
        public LentProductRepository(IDbContext context) : base(context)
        {
        }
    }
}