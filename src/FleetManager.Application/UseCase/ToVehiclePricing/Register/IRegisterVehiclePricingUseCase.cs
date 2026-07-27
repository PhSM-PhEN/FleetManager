using FleetManager.Communication.Request.ToVehiclePricing;
using FleetManager.Communication.Response.ToVehiclePricing;

namespace FleetManager.Application.UseCase.ToVehiclePricing.Register
{
    public interface IRegisterVehiclePricingUseCase
    {
        Task<ResponseVehiclePricingJson> Execute(RequestVehiclePricingJson request);
    }
}
