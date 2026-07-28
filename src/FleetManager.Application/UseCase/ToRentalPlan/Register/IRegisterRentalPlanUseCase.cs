using FleetManager.Communication.Request.ToRentalPlan;
using FleetManager.Communication.Response.ToRentalPlan;

namespace FleetManager.Application.UseCase.ToRentalPlan.Register
{
    public interface IRegisterRentalPlanUseCase
    {
        Task<ResponseRentalPlanJson> Execute(RequestRentalPlanJson request);
    }
}
