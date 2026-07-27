using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToVehiclePricing;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToVehiclePricing
{
    internal class VehiclePricingRepository(FleetManagerDbContext dbContext) : IVehiclePricingWriteOnlyRepository, IVehiclePricingReadOnlyRepository
    {
        public async Task Add(VehiclePricing vehiclePricing)
        {
            await dbContext.VehiclePricings.AddAsync(vehiclePricing);
        }

        public async Task<VehiclePricing?> GetById(long vehicleId)
        {
            return await dbContext.VehiclePricings
                .FirstOrDefaultAsync(p => p.Id == vehicleId);
        }

        async Task<VehiclePricing?> IVehiclePricingReadOnlyRepository.GetById(long vehicleId)
        {
            return await dbContext.VehiclePricings.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == vehicleId);
        }

        public void Update(VehiclePricing vehiclePricing)
        {
            dbContext.Update(vehiclePricing);
        }

        public async Task<(List<VehiclePricing>, int TotalCount)> GetAll(int pageNumber, int pageSize)
        {
            var query = dbContext.VehiclePricings.AsNoTracking();
            var totalCount = await query.CountAsync();
            var vehiclePricing = await query
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
            return (vehiclePricing, totalCount);
        }
    }
}
