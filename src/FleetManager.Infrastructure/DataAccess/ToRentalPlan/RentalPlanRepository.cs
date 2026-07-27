using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToRentalPlan;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToRentalPlan
{
    internal class RentalPlanRepository(FleetManagerDbContext dbContext) : IRentalPlanWriteOnlyRepository, IRentalPlanReadOnlyRepository
    {
        public async Task Add(RentalPlan vehiclePricing)
        {
            await dbContext.RentalPlans.AddAsync(vehiclePricing);
        }

        public async Task<RentalPlan?> GetById(long vehicleId)
        {
            return await dbContext.RentalPlans
                .FirstOrDefaultAsync(p => p.Id == vehicleId);
        }

        async Task<RentalPlan?> IRentalPlanReadOnlyRepository.GetById(long vehicleId)
        {
            return await dbContext.RentalPlans.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == vehicleId);
        }

        public void Update(RentalPlan vehiclePricing)
        {
            dbContext.Update(vehiclePricing);
        }

        public async Task<(List<RentalPlan>, int TotalCount)> GetAll(int pageNumber, int pageSize)
        {
            var query = dbContext.RentalPlans.AsNoTracking();
            var totalCount = await query.CountAsync();
            var vehiclePricing = await query
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
            return (vehiclePricing, totalCount);
        }

        public async Task Delete(long id)
        {   var rentalPlan = await dbContext.RentalPlans.FirstOrDefaultAsync(p => p.Id == id);

            dbContext.Remove(rentalPlan!.Id);
        }
    }
}
