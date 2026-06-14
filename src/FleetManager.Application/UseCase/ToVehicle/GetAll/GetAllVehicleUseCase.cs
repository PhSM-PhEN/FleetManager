using AutoMapper;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.GetAll
{
    public class GetAllVehicleUseCase(IMapper mapper, IVehicleReadOnlyRepository vehicleReadOnly) : IGetAllVehicleUseCase
    {
     
     
        public async Task<ResponseListVehicleJson> Execute()
        {
            var vehicles = await vehicleReadOnly.GetAll();
            
            if(vehicles.Count == 0)
            {
                throw new NotFoundException (ResourceErrorMessages.VEHICLE_NOT_FOUND);
            }

            return new ResponseListVehicleJson
            {
                Vehicle = mapper.Map<List<ResponseVehicleJson>>(vehicles)
            };

        }
    }
}
