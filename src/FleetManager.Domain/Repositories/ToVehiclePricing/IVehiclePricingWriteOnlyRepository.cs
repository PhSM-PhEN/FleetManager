using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToVehiclePricing
{
    public interface IVehiclePricingWriteOnlyRepository
    {
        Task Add(VehiclePricing vehiclePricing);
        Task<VehiclePricing?> GetById(long vehicleId);
        void Update(VehiclePricing vehiclePricing);
    }
}
