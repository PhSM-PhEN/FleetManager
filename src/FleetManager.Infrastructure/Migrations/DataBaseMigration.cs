using FleetManager.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManager.Infrastructure.Migrations
{
    public static class DataBaseMigration
    {
        public static async Task MigrateDataBase(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<FleetManagerDbContext>();
            await dbContext.Database.MigrateAsync();
        }

    }
}
