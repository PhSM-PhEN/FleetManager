using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToRentalPlan;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToRentalPlan
{
    internal class RentalPlanRepository(FleetManagerDbContext dbContext) : IRentalPlanWriteOnlyRepository, IRentalPlanReadOnlyRepository
    {
        public async Task Add(RentalPlan rentalPlan)
        {
            await dbContext.RentalPlans.AddAsync(rentalPlan);
        }

        public async Task<RentalPlan?> GetById(long id)
        {
            return await dbContext.RentalPlans
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        async Task<RentalPlan?> IRentalPlanReadOnlyRepository.GetById(long id)
        {
            return await dbContext.RentalPlans.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Update(RentalPlan rentalPlan)
        {
            dbContext.Update(rentalPlan);
        }

        public async Task<(List<RentalPlan>, int TotalCount)> GetAll(int pageNumber, int pageSize)
        {
            var query = dbContext.RentalPlans.AsNoTracking();
            var totalCount = await query.CountAsync();
            var rentalPlan = await query
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
            return (rentalPlan, totalCount);
        }

        public async Task Delete(long id)
        {
            var rentalPlan = await dbContext.RentalPlans.FindAsync(id);

            dbContext.Remove(rentalPlan!);
        }
    }
}
