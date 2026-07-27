using FleetManager.Communication.Request.ToRentalPlan;
using FleetManager.Communication.Response.ToRentalPlan;

namespace FleetManager.Application.UseCase.ToVehiclePricing.Register
{
    public interface IRegisterRentalPlanUseCase
    {
        Task<ResponseRentalPlanJson> Execute(RequestRentalPlanJson request);
    }
}
