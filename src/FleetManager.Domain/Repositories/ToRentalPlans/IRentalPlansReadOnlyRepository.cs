using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToRentalPlans
{
    public interface IRentalPlansReadOnlyRepository
    {
        Task<RentalPlan?> GetById(long id);
        Task<List<RentalPlan>> GetAll();
    }
}
