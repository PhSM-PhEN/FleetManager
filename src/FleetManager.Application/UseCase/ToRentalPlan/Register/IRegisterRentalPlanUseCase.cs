using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToRentalPlan.Register
{
    public interface IRegisterRentalPlanUseCase
    {
        Task<ResponseRentalPlanJson> Execute(RequestRentalPlansJson request);
    }
}
