using FleetManager.Communication.Request.ToRentalPlan;

namespace FleetManager.Application.UseCase.ToRentalPlan.Update
{
    public interface IUpdateRentalPlanUseCase
    {
        Task Execute(long vehicleId, RequestRentalPlanJson request);
    }
}
