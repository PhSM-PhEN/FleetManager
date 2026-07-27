using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToVehiclePricing
{
    public interface IVehiclePricingReadOnlyRepository
    {
        Task<VehiclePricing?> GetByVehicleId(long vehicleId);
    }
}
