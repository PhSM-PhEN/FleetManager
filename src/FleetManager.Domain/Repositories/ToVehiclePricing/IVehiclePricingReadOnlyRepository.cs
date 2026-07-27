using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToVehiclePricing
{
    public interface IVehiclePricingReadOnlyRepository
    {
        Task<VehiclePricing?> GetById(long vehicleId);
        Task<(List<VehiclePricing>, int TotalCount)> GetAll(int pageNumber, int pageSize);
    }
}
