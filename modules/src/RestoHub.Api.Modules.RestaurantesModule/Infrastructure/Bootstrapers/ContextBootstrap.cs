using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestoHub.Api.Modules.RestaurantesModule.Data.Context;
using System.Data;

namespace RestoHub.Api.Modules.RestaurantesModule.Infrastructure.Bootstrapers
{
    public static class ContextBootstrap
    {
        public static IServiceCollection ConfigureContextDb(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddTransient<IDbConnection>(b =>
            {
                return new SqlConnection(connectionString);
            });

            services.AddDbContextPool<RestaurantesDbContext>(options =>
                options.UseSqlServer(
                    connectionString
                )
            );

            return services;
        }

        public static void MigrateDatabaseOnStartup(
            this IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<RestaurantesDbContext>();

            context.Database.Migrate();
        }
    }
}
