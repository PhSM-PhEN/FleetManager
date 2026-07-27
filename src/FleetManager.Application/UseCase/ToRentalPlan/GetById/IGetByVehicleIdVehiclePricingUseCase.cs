using FleetManager.Communication.Response.ToRentalPlan;

namespace FleetManager.Application.UseCase.ToVehiclePricing.GetByVehicleId
{
    public interface IGetByVehicleIdVehiclePricingUseCase
    {
        Task<ResponseRentalPlanJson> Execute(long id);
    }
}
