using FleetManager.Application.Extensions;
using FleetManager.Communication.Response.ToVehiclePricing;
using FleetManager.Domain.Repositories.ToVehiclePricing;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehiclePricing.GetByVehicleId
{
    public class GetByVehicleIdVehiclePricingUseCase(IVehiclePricingReadOnlyRepository repository) : IGetByVehicleIdVehiclePricingUseCase
    {
        public async Task<ResponseVehiclePricingJson> Execute(long vehicleId)
        {
            var pricing = await repository.GetByVehicleId(vehicleId) ??
                throw new NotFoundException(ResourceErrorMessages.VEHICLE_PRICING_NOT_FOUND);

            return pricing.ToResponse();
        }
    }
}
