using AutoMapper;
using FleetManager.communication.Responses;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.GetById
{
    public class GetByIdVehicleUseCase(IMapper mapper , 
        IVehicleReadOnlyRepository vehicleReadOnly) : IGetByIdVehicleUseCase
    {

        public async Task<ResponseVehicleByIdJson> Execute(long id)
        {
            var respone = await vehicleReadOnly.GetById(id);
            return mapper.Map<ResponseVehicleByIdJson>(respone);
        }
    }
}
