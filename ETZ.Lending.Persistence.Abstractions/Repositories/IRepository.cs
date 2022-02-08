using System.Threading;
using System.Threading.Tasks;
using ETZ.Lending.Persistence.Abstractions.Entities;

namespace ETZ.Lending.Persistence.Abstractions.Repositories
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : class, IEntity
    {
        TEntity Create(TEntity entity);
        Task<TEntity> CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(object id);
        Task DeleteAsync(object id);
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
    }
}