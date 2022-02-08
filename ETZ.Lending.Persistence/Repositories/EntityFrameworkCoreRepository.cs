using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ETZ.Lending.Persistence.Abstractions.Entities;
using ETZ.Lending.Persistence.Abstractions.Repositories;
using ETZ.Lending.Persistence.Contexts;

namespace ETZ.Lending.Persistence.Repositories
{
    public class EntityFrameworkCoreRepository<TEntity> : EntityFrameworkCoreReadOnlyRepository<TEntity>, IRepository<TEntity>
        where TEntity : class, IEntity
    {
        public EntityFrameworkCoreRepository(IDbContext context) : base(context)
        {
        }

        public TEntity Create(TEntity entity)
        {
            var created = Context.Set<TEntity>().Add(entity);
            return created.Entity;
        }
        
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var created = await Context.Set<TEntity>().AddAsync(entity);
            return created.Entity;
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;

            if (entity is IModificationDateTime time)
            {
                Context.Entry(time).Property(static x => x.CreatedAt).IsModified = false;
            }
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }
        
        public void Delete(object id)
        {
            var entity = Find(id);
            Delete(entity);
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await FindAsync(id);
            Delete(entity);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }
    }
}