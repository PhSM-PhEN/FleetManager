using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToRentalPlan;

namespace FleetManager.Application.UseCase.ToRentalPlan.GetAll
{
    public interface IGetAllRentalPlanUseCase
    {
        Task<ResponsePaginatedJson<ResponseRentalPlanJson>> Execute(int pageNumber, int pageSize);
    }
}
