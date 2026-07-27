using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToRentalPlan;

namespace FleetManager.Application.UseCase.ToVehiclePricing.GetAll
{
    public interface IGetAllVehiclePricing
    {
        Task<ResponsePaginatedJson<ResponseRentalPlanJson>> Execute(int pageNumber, int pageSize);
    }
}
