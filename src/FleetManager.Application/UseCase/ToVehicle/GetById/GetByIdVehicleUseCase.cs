using AutoMapper;
using FleetManager.communication.Resposnes.ToVehicle;
using FleetManager.Domain.Repositories.ToVehicle;

namespace FleetManager.Application.UseCase.ToVehicle.GetById
{
    public class GetByIdVehicleUseCase(IMapper mapper , 
        IVehicleReadOnlyRepository vehicleReadOnly) : IGetByIdVehicleUseCase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IVehicleReadOnlyRepository _vehicleReadOnly = vehicleReadOnly;

        public async Task<ResponseVehicleByIdJson> Execute(long id)
        {
            var respone = await _vehicleReadOnly.GetById(id);

            return _mapper.Map<ResponseVehicleByIdJson>(respone);
        }
    }
}
