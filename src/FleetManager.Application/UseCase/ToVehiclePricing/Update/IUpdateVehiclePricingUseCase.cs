using FleetManager.Communication.Request.ToVehiclePricing;

namespace FleetManager.Application.UseCase.ToVehiclePricing.Update
{
    public interface IUpdateVehiclePricingUseCase
    {
        Task Execute(long vehicleId, RequestVehiclePricingJson request);
    }
}
