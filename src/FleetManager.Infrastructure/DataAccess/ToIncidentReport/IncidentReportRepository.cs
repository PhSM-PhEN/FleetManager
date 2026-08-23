using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToIncidentReport;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToIncidentReport
{
    public class IncidentReportRepository(FleetManagerDbContext dbContext) : IIncidentReportWriteOnlyRepository, IIncidentReportReadOnlyRepository
    {
        public async Task Add(IncidentReport incidentReport)
        {
            await dbContext.IncidentReports.AddAsync(incidentReport);
        }

        public Task Delete(IncidentReport incidentReport)
        {
            dbContext.IncidentReports.Remove(incidentReport);
            return Task.CompletedTask;
        }

        public async Task<(List<IncidentReport>, int TotalCount)> GetAll(int pageNumber, int PageSize)
        {
            var query = dbContext.IncidentReports.AsNoTracking();
            var totalCount = await query.CountAsync();
            var incidentReport = await query
                                .Skip((pageNumber - 1) * PageSize)
                                .Take(PageSize)
                                .ToListAsync();
            return(incidentReport, totalCount);
        }
        async Task<IncidentReport?> IIncidentReportReadOnlyRepository.GetById(long id)
        {
            return await dbContext.IncidentReports.AsNoTracking()
                            .Include(ir => ir.Contract)
                                .ThenInclude(c => c.Tenant)
                                    .ThenInclude(t => t.Address)
                            .Include(ir => ir.Contract)
                                .ThenInclude(c => c.Vehicle)
                                    .ThenInclude(v => v.Company)
                                        .ThenInclude(comp => comp.Address)
                            .Include(ir => ir.Contract)
                                .ThenInclude(c => c.Vehicle)
                                    .ThenInclude(v => v.RentalPlan)
                            .Include(ir => ir.Vehicle)
                                .ThenInclude(v => v.Company)
                                    .ThenInclude(comp => comp.Address)
                            .Include(ir => ir.Vehicle)
                                .ThenInclude(v => v.RentalPlan)
                            .FirstOrDefaultAsync(ir => ir.Id == id);
        }
        public async Task<IncidentReport?> GetById(long id)
        {
            return await dbContext.IncidentReports.FirstOrDefaultAsync(ir => ir.Id == id);
        }

        public void Update(IncidentReport incidentReport)
        {
            dbContext.IncidentReports.Update(incidentReport);
        }
    }
}
