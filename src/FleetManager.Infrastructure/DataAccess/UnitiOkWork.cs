using FleetManager.Domain.Repositories;

namespace FleetManager.Infrastructure.DataAccess
{
    internal class UnitiOkWork(FleetManagerDbContext dbContext) : IUnitOfWork
    {
        private readonly FleetManagerDbContext _fleetManagerDbContext = dbContext;
        public async Task Commit()
        {
            await _fleetManagerDbContext.SaveChangesAsync();
        }
    }
}
