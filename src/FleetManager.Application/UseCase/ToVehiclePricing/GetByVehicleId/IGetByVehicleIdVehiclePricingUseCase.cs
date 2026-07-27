using FleetManager.Communication.Response.ToVehiclePricing;

namespace FleetManager.Application.UseCase.ToVehiclePricing.GetByVehicleId
{
    public interface IGetByVehicleIdVehiclePricingUseCase
    {
        Task<ResponseVehiclePricingJson> Execute(long vehicleId);
    }
}
