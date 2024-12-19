using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestoHub.Api.Modules.UsersModule.Data.Context;
using System.Data;

namespace RestoHub.Api.Modules.UsersModule.Infrastructure.Bootstrapers
{
    public static class ContextBootstrap
    {
        public static IServiceCollection ConfigureContextDb(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Users");
            services.AddTransient<IDbConnection>(b =>
            {
                return new SqlConnection(connectionString);
            });

            services.AddDbContextPool<UsersDbContext>(options =>
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
            using var context = scope.ServiceProvider.GetRequiredService<UsersDbContext>();

            context.Database.Migrate();
        }
    }
}
