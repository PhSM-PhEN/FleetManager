using FleetManager.Application.Extensions;
using FleetManager.Communication.Response.ToVehicle;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.GetById
{
    public class GetByIdVehicleUseCase(IVehicleReadOnlyRepository repository) : IGetByIdVehicleUseCase
    {
        public async Task<ResponseVehicleJson> Execute(long id)
        {
            var vehicle = await repository.GetById(id) ??
                throw new NotFoundException("Vehicle not found");
                
            return vehicle.ToInfoResponse();
        }
    }
}
