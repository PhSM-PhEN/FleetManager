using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToRentalPlan.GetAll
{
    public interface IGetAllRentalPlanUseCase
    {
        Task<List<ResponseShortRentalPlansJson>> Execute();
    }
}
