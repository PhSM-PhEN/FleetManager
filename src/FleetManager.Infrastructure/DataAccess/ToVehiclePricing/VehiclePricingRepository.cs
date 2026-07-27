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

        public async Task<VehiclePricing?> GetByVehicleId(long vehicleId)
        {
            return await dbContext.VehiclePricings
                .FirstOrDefaultAsync(p => p.VehicleId == vehicleId);
        }

        async Task<VehiclePricing?> IVehiclePricingReadOnlyRepository.GetByVehicleId(long vehicleId)
        {
            return await dbContext.VehiclePricings.AsNoTracking()
                .FirstOrDefaultAsync(p => p.VehicleId == vehicleId);
        }

        public void Update(VehiclePricing vehiclePricing)
        {
            dbContext.Update(vehiclePricing);
        }
    }
}
