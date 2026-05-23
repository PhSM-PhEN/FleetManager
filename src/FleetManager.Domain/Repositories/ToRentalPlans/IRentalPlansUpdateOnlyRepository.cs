using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToRentalPlans
{
    public interface IRentalPlansUpdateOnlyRepository
    {
        Task<RentalPlan?> GetById(int id);
        void Update(RentalPlan rentalPlan);
    }
}
