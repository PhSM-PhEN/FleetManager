using AutoMapper;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToVehicle;

namespace FleetManager.Application.UseCase.ToVehicle.GetAll
{
    public class GetAllVehicleUseCase(IMapper mapper, IVehicleReadOnlyRepository vehicleReadOnly) : IGetAllVehicleUseCase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IVehicleReadOnlyRepository _vehicleReadOnly = vehicleReadOnly;
     
        public async Task<ResponseListVehicleJson> Execute()
        {
            var vehicles = await _vehicleReadOnly.GetAll();
            
            

            return new ResponseListVehicleJson
            {
                Vehicle = _mapper.Map<List<ResponseVehicleJson>>(vehicles)
            };

        }
    }
}
