using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToRentalPlans;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRentalPlan.Disable
{
    public class DisableRentalPlanUseCase(IUnitOfWork unitOfWork, IRentalPlansUpdateOnlyRepository updateOnlyRepository) : IDisableRentalPlanUseCase
    {
        public async Task Execute(int id)
        {
            var rentalPlan = await updateOnlyRepository.GetById(id) 
                ?? throw new NotFoundException(ResourceErrorMessages.RENTAL_PLAN_NOT_FOUND);

            rentalPlan.Disable();

            updateOnlyRepository.Update(rentalPlan);
            await unitOfWork.Commit();
        }
    }
}
