using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ETZ.Lending.Persistence.Abstractions.Entities;
using ETZ.Lending.Persistence.Abstractions.Repositories;
using ETZ.Lending.Persistence.Contexts;
using ETZ.Lending.Persistence.Extensions;

namespace ETZ.Lending.Persistence.Repositories
{
    public class FileSystemRepository : IFileSystemRepository
    {
        private readonly IFileContext _context;
        private readonly IDictionary<string, FileSystemEntity> _cache;

        public FileSystemRepository(IFileContext context)
        {
            _context = context;
            _cache = new Dictionary<string, FileSystemEntity>();
        }

        private IEnumerable<FileSystemEntity> IndexFiles()
        {
            var uncached = _context.GetFiles().Where(file => !_cache.ContainsKey(file.FullName));
            foreach (var file in uncached)
            {
                var entity = new FileSystemEntity {Id = file.Name, Path = file.FullName};
                _cache.Add(file.FullName, entity);
            }

            return _cache.Values;
        }

        private IQueryable<FileSystemEntity> GetQueryable(
            Expression<Func<FileSystemEntity, bool>> filter = null,
            Func<IQueryable<FileSystemEntity>, IOrderedQueryable<FileSystemEntity>> orderBy = null,
            int? skip = null,
            int? take = null)
        {
            var query = IndexFiles().AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public IEnumerable<FileSystemEntity> AllToList(
            Func<IQueryable<FileSystemEntity>, IOrderedQueryable<FileSystemEntity>> orderBy = null,
            string[] includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return GetQueryable(null, orderBy, skip, take).IncludeData().ToList();
        }

        public Task<IEnumerable<FileSystemEntity>> AllToListAsync(
            Func<IQueryable<FileSystemEntity>, IOrderedQueryable<FileSystemEntity>> orderBy = null,
            string[] includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return Task.FromResult(AllToList(orderBy, includeProperties, skip, take));
        }

        public IEnumerable<FileSystemEntity> ToList(
            Expression<Func<FileSystemEntity, bool>> filter = null,
            Func<IQueryable<FileSystemEntity>, IOrderedQueryable<FileSystemEntity>> orderBy = null,
            string[] includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return GetQueryable(filter, orderBy, skip, take).IncludeData().ToList();
        }

        public Task<IEnumerable<FileSystemEntity>> ToListAsync(
            Expression<Func<FileSystemEntity, bool>> filter = null,
            Func<IQueryable<FileSystemEntity>, IOrderedQueryable<FileSystemEntity>> orderBy = null,
            string[] includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return Task.FromResult(ToList(filter, orderBy, includeProperties, skip, take));
        }

        public FileSystemEntity SingleOrDefault(Expression<Func<FileSystemEntity, bool>> filter = null, string[] includeProperties = null)
        {
            return GetQueryable(filter).IncludeData().SingleOrDefault();
        }

        public Task<FileSystemEntity> SingleOrDefaultAsync(Expression<Func<FileSystemEntity, bool>> filter = null, string[] includeProperties = null)
        {
            return Task.FromResult(SingleOrDefault(filter, includeProperties));
        }

        public FileSystemEntity FirstOrDefault(
            Expression<Func<FileSystemEntity, bool>> filter = null,
            Func<IQueryable<FileSystemEntity>, IOrderedQueryable<FileSystemEntity>> orderBy = null,
            string[] includeProperties = null)
        {
            return GetQueryable(filter, orderBy).IncludeData().FirstOrDefault();
        }

        public Task<FileSystemEntity> FirstOrDefaultAsync(
            Expression<Func<FileSystemEntity, bool>> filter = null,
            Func<IQueryable<FileSystemEntity>, IOrderedQueryable<FileSystemEntity>> orderBy = null,
            string[] includeProperties = null)
        {
            return Task.FromResult(FirstOrDefault(filter, orderBy, includeProperties));
        }

        public FileSystemEntity Find(object id)
        {
            var fileName = Path.GetFileName(id.ToString());
            return GetQueryable(e => e.Id == fileName).IncludeData().SingleOrDefault();
        }

        public Task<FileSystemEntity> FindAsync(object id)
        {
            return Task.FromResult(Find(id));
        }

        public int Count(Expression<Func<FileSystemEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }

        public Task<int> CountAsync(Expression<Func<FileSystemEntity, bool>> filter = null)
        {
            return Task.FromResult(GetQueryable(filter).Count());
        }

        public bool Any(Expression<Func<FileSystemEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Any();
        }

        public Task<bool> AnyAsync(Expression<Func<FileSystemEntity, bool>> filter = null)
        {
            return Task.FromResult(GetQueryable(filter).Any());
        }

        public FileSystemEntity Create(FileSystemEntity entity)
        {
            entity.Path = Path.Combine(_context.FullName, entity.Id ?? Guid.NewGuid().ToString());

            _context.Write(entity.Path, entity.Stream);

            return entity;
        }

        public Task<FileSystemEntity> CreateAsync(FileSystemEntity entity)
        {
            return Task.FromResult(Create(entity));
        }

        public void Update(FileSystemEntity entity)
        {
            _context.Write(entity.Path, entity.Stream);
        }

        public void Delete(FileSystemEntity entity)
        {
            _context.Delete(entity.Path);
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
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}