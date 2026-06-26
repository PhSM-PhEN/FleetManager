using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToRentalPlan.GetById
{
    public interface IGetByIdRentalPlanUseCase
    {
        Task<ResponseRentalPlanJson> Execute(long id);
    }
}
