using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Northwind.Domain
{
    public static class DataModule
    {
        public static IServiceCollection AddDataModule(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DbConnection"));
                //options.EnableSensitiveDataLogging();

                options.UseSnakeCaseNamingConvention();
            });
            return service;
        }
    }
}