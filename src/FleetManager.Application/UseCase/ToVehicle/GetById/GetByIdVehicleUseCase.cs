using FleetManager.Application.Extensions;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.GetById
{
    public class GetByIdVehicleUseCase(
        IVehicleReadOnlyRepository vehicleReadOnly) : IGetByIdVehicleUseCase
    {

        public async Task<ResponseVehicleByIdJson> Execute(long id)
        {
            var vehicle = await vehicleReadOnly.GetById(id)
             ?? throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);
            
            return vehicle.ToDetailResponse();
        }
    }
}
