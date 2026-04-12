using AutoMapper;
using FleetManager.communication.Resposnes;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Application.UseCase.ToVehicle.GetAll
{
    public class GetAllVehicleUseCase(IMapper mapper, IVehicleReadOnlyRepository vehicleReadOnly) : IGetAllVehicleUseCase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IVehicleReadOnlyRepository _vehicleReadOnly = vehicleReadOnly;
        private readonly FleetManagerDbContext _dbContext;
        public async Task<ResponseAllVehicleJson> Execute()
        {
            var vehicles = await _vehicleReadOnly.GetAll();
            _dbContext.Vehicles.AsNoTracking().Select(v => new ResponseVehicleJson
            {
                Id = v.Id,
                LicensePlate = v.LicensePlate,
                
                Brand = v.Brand,
                FuelType = v.FuelType.ToString(),
                TransmissionType = v.TransmissionType,
            }).ToListAsync();

            return new ResponseAllVehicleJson
            {
                Vehicle = _mapper.Map<List<ResponseVehicleJson>>(vehicles)
            };

        }
    }
}
