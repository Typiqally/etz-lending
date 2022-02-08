using ETZ.Lending.Persistence.Abstractions.Entities;
using ETZ.Lending.Persistence.Abstractions.Repositories;
using ETZ.Lending.Persistence.Contexts;

namespace ETZ.Lending.Persistence.Repositories
{
    public class ProductRepository : EntityFrameworkCoreRepository<ProductEntity>, IProductRepository
    {
        public ProductRepository(IDbContext context) : base(context)
        {
        }
    }
}