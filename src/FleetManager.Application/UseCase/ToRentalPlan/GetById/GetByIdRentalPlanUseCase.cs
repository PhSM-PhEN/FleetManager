using FleetManager.Application.Extensions;
using FleetManager.Communication.Response.ToRentalPlan;
using FleetManager.Domain.Repositories.ToRentalPlan;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehiclePricing.GetByVehicleId
{
    public class GetByIdRentalPlanUseCase(IRentalPlanReadOnlyRepository repository) : IGetByVehicleIdVehiclePricingUseCase
    {
        public async Task<ResponseRentalPlanJson> Execute(long id)
        {
            var rentalPlan = await repository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.RENTAL_PLAN_NOT_FOUND);

            return rentalPlan.ToResponse();
        }
    }
}
