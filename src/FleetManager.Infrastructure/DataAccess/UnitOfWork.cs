using FleetManager.Domain.Repositories;

namespace FleetManager.Infrastructure.DataAccess
{
    internal class UnitOfWork(FleetManagerDbContext dbContext) : IUnitOfWork
    {
        public async Task Commit()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
