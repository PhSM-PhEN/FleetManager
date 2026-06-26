using FleetManager.Application.Extensions;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToRentalPlans;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRentalPlan.GetById
{
    public class GetByIdRentalPlanUseCase(IRentalPlansReadOnlyRepository repository) : IGetByIdRentalPlanUseCase
    {
        public async Task<ResponseRentalPlanJson> Execute(long id)
        {
            var rentalPlan = await repository.GetById(id) 
                ?? throw new NotFoundException(ResourceErrorMessages.RENTAL_PLAN_NOT_FOUND);


            return rentalPlan.ToDetailResponse();
        }
    }
}
