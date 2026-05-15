using AutoMapper;
using FleetManager.communication.Resposnes.ToVehicle;
using FleetManager.Domain.Repositories.ToVehicle;

namespace FleetManager.Application.UseCase.ToVehicle.GetAll
{
    public class GetAllVehicleUseCase(IMapper mapper, IVehicleReadOnlyRepository vehicleReadOnly) : IGetAllVehicleUseCase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IVehicleReadOnlyRepository _vehicleReadOnly = vehicleReadOnly;
     
        public async Task<ResponseAllVehicleJson> Execute()
        {
            var vehicles = await _vehicleReadOnly.GetAll();
            

            return new ResponseAllVehicleJson
            {
                Vehicle = _mapper.Map<List<ResponseVehicleJson>>(vehicles)
            };

        }
    }
}
