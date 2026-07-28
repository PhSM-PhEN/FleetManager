using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToRentalPlan;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRentalPlan.Delete
{
    public class DeleteRentalPlanUseCase(IRentalPlanWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDeleteRentalPlanUseCase
    {
        public async Task Execute(long id)
        {
            var rentalPlan = await repository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.RENTAL_PLAN_NOT_FOUND);

            await repository.Delete(rentalPlan.Id);
            await unitOfWork.Commit();
        }
    }
}
