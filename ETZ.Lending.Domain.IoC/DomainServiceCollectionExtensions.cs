using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ETZ.Lending.Domain.Abstractions.Services;
using ETZ.Lending.Domain.Profiles;
using ETZ.Lending.Domain.Services;
using ETZ.Lending.Persistence.IoC;

namespace ETZ.Lending.Domain.IoC
{
    public static class DomainServiceCollectionExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);

            // AutoMapper
            services.AddAutoMapper(static config =>
            {
                config.AddProfile<FileMappingProfile>();
                config.AddProfile<ProductMappingProfile>();
                config.AddProfile<LentProductMappingProfile>();
            });

            services.AddScoped<IFileDomainService, FileDomainService>();
            services.AddScoped<IProductDomainService, ProductDomainService>();
            services.AddScoped<ILentProductDomainService, LentProductDomainService>();
            
            services.AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();

            return services;
        }
    }
}