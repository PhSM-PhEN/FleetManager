using FleetManager.Communication.Request.ToRentalPlan;

namespace FleetManager.Application.UseCase.ToVehiclePricing.Update
{
    public interface IUpdateRentalPlanUseCase
    {
        Task Execute(long vehicleId, RequestRentalPlanJson request);
    }
}
