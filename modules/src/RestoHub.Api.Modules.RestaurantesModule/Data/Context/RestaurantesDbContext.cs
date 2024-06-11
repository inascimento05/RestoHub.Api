using Microsoft.EntityFrameworkCore;

namespace RestoHub.Api.Modules.RestaurantesModule.Data.Context
{
    public class RestaurantesDbContext : DbContext
    {
        public RestaurantesDbContext(DbContextOptions<RestaurantesDbContext> options)
            : base(options)
        {
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            base.ConfigureConventions(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            OnSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void OnSaveChanges()
        {
        }
    }
}
