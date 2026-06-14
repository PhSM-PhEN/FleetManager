using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToRentalPlans;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRentalPlan.Delete;

public class DeleteRentalPlanUseCase(IUnitOfWork unitOfWork, IRentalPlansUpdateOnlyRepository updateOnlyRepository) : IDeleteRentalPlanUseCase
{
    public async Task Execute(int id)
    {
        var rentalPlan = await updateOnlyRepository.GetById(id);
        if (rentalPlan is null)
            throw new NotFoundException(ResourceErrorMessages.RENTAL_PLAN_NOT_FOUND);

        rentalPlan.Disable();
        updateOnlyRepository.Update(rentalPlan);
        await unitOfWork.Commit();
    }
}
