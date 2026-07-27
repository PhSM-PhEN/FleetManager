using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToRentalPlan
{
    public interface IRentalPlanReadOnlyRepository
    {
        Task<RentalPlan?> GetById(long vehicleId);
        Task<(List<RentalPlan>, int TotalCount)> GetAll(int pageNumber, int pageSize);
    }
}
