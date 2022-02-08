using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ETZ.Lending.Presentation.WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(static webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}