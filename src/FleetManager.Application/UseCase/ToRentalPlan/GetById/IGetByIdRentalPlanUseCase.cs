using FleetManager.Communication.Response.ToRentalPlan;

namespace FleetManager.Application.UseCase.ToRentalPlan.GetById
{
    public interface IGetByIdRentalPlanUseCase
    {
        Task<ResponseRentalPlanJson> Execute(long id);
    }
}
