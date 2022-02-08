using ETZ.Lending.Presentation.WebApi.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ETZ.Lending.Domain.IoC;

namespace ETZ.Lending.Presentation.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(static options =>
            {
                options.AddDefaultPolicy(static builder =>
                {
                    builder.WithOrigins("http://localhost:8080", "http://localhost:8081");
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });
            
            services.AddAutoMapper(static config =>
            {
                config.AddProfile<ProductViewModelMappingProfile>();
                config.AddProfile<LentProductViewModelMappingProfile>();
            });
            
            services.AddDomain(Configuration);

            services.AddSwaggerGen(static c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "ETZ Lending Web API", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(static c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ETZ Lending Web API v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(static endpoints => { endpoints.MapControllers(); });
        }
    }
}