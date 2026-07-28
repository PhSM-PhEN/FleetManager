using FleetManager.Communication.Response.ToRentalPlan;

namespace FleetManager.Application.UseCase.ToRentalPlan.GetById
{
    public interface IGetByRentalPlanUseCase
    {
        Task<ResponseRentalPlanJson> Execute(long id);
    }
}
