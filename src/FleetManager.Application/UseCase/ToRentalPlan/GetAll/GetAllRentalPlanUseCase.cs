using FleetManager.Application.Extensions;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToRentalPlans;
using FleetManager.Exception.ExceptionBase;
namespace FleetManager.Application.UseCase.ToRentalPlan.GetAll
{
    public class GetAllRentalPlanUseCase(IRentalPlansReadOnlyRepository repository) : IGetAllRentalPlanUseCase
    {
        public async Task<List<ResponseShortRentalPlansJson>> Execute()
        {
            var rentalPlan = await repository.GetAll() ??
                throw new NotFoundException(ResourceErrorMessages.RENTAL_NOT_FOUND);

            return rentalPlan.ToResponse();

        }
    }
}
