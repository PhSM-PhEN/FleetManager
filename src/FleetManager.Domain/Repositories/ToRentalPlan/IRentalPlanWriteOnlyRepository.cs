using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToRentalPlan
{
    public interface IRentalPlanWriteOnlyRepository
    {
        Task Add(RentalPlan rentalPlan);
        Task<RentalPlan?> GetById(long id);
        void Update(RentalPlan rentalPlan);
        Task Delete(RentalPlan rentalPlan);
    }
}
