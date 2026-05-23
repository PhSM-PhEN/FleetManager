using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToRentalPlans
{
    public interface IRentalPlansWriteOnlyRepository
    {
        Task Add(RentalPlan rentalPlan);
        Task Delete(int id);
    }
}
