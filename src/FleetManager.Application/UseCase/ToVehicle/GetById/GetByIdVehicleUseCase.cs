using AutoMapper;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToVehicle;

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
