using FleetManager.communication.Requests;
using FleetManager.communication.Responses;

namespace FleetManager.Application.UseCase.ToRentalPlan.Register
{
    public interface IRegisterRentalPlanUseCase
    {
        Task<ResponseRentalPlanJson> Execute(RequestRentalPlansJson request);
    }
}
