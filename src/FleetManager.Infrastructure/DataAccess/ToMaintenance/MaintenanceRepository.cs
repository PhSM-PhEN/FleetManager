using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToMaintenance;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToMaintenance
{
    public class MaintenanceRepository(FleetManagerDbContext dbContext) : IMaintenanceWriteOnlyRepository, IMaintenanceReadOnlyRepository
    {
        public async Task Add(Maintenance maintenance)
        {
           await  dbContext.Maintenances.AddAsync(maintenance);
        }

        public Task Delete(Maintenance maintenance)
        {
            dbContext.Maintenances.Remove(maintenance);
            return Task.CompletedTask;
        }

        public async Task<(List<Maintenance>, int totalCount)> GetAll(int pagenumber, int pageSize)
        {
            var query =  dbContext.Maintenances.AsNoTracking();
            var totalCount = await query.CountAsync();
            var Maintenance = await query.
                                    Skip((pagenumber - 1 ) * pageSize).
                                    Take(pageSize).
                                    ToListAsync();
            return (Maintenance, totalCount);
        }

        public async Task<Maintenance?> GetById(long id)
        {
            return await dbContext.Maintenances.FirstOrDefaultAsync(m => m.Id == id);
        }
        async Task<Maintenance?> IMaintenanceReadOnlyRepository.GetById(long id)
        {
            return await dbContext.Maintenances.AsNoTracking()
                        .Include(v => v.Vehicle)
                        .Include(ir => ir.IncidentReport)
                        .FirstOrDefaultAsync(m => m.Id == id);
        }

        public void Update(Maintenance maintenance)
        {
            dbContext.Update(maintenance);
        }
    }
}
