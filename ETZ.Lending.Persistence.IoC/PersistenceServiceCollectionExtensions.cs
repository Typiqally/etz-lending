using System.IO;
using ETZ.Lending.Persistence.Abstractions.Repositories;
using ETZ.Lending.Persistence.Contexts;
using ETZ.Lending.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETZ.Lending.Persistence.IoC
{
    public static class PersistenceServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // Entity Framework and contexts
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), static o => o.MigrationsAssembly("ETZ.Lending.Persistence")));

            services.AddScoped<IDbContext>(static provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IFileContext>(static _ => new FileContext(new DirectoryInfo("Uploads")));

            // Repositories
            services.AddScoped<IFileSystemRepository, FileSystemRepository>();
            services.AddScoped<IFileManifestRepository, FileManifestRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ILentProductRepository, LentProductRepository>();

            return services;
        }
    }
}