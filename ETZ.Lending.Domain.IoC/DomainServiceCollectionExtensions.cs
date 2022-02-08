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
                config.AddProfile<ProductMappingProfile>();
            });
            
            services.AddScoped<IProductDomainService, ProductDomainService>();
            services.AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();

            return services;
        }
    }
}