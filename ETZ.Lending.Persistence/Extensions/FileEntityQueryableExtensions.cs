using System.IO;
using System.Linq;
using ETZ.Lending.Persistence.Abstractions.Entities;

namespace ETZ.Lending.Persistence.Extensions
{
    public static class FileEntityQueryableExtensions
    {
        public static IQueryable<FileSystemEntity> IncludeData(this IQueryable<FileSystemEntity> queryable)
        {
            foreach (var entity in queryable)
            {
                entity.Stream = new FileStream(entity.Path, FileMode.Open);
            }

            return queryable;
        }
    }
}