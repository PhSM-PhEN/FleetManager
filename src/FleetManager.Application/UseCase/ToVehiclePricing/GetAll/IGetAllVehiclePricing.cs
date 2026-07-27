using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToVehiclePricing;

namespace FleetManager.Application.UseCase.ToVehiclePricing.GetAll
{
    public interface IGetAllVehiclePricing
    {
        Task<ResponsePaginatedJson<ResponseVehiclePricingJson>> Execute(int pageNumber, int pageSize);
    }
}
