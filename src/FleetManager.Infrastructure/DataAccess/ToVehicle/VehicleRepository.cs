using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToVehicle;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToVehicle
{
    internal class VehicleRepository(FleetManagerDbContext context) : IVehicleWriteOnlyRepository, IVehicleReadOnlyRepository, IVehicleUpdateOnlyRepository
    {
        private readonly FleetManagerDbContext _dbContext = context;
        public async Task Add(Vehicle vehicle)
        {
            await _dbContext.Vehicles.AddAsync(vehicle);
        }

        public async Task Delete(long id)
        {
            var result =  await _dbContext.Vehicles.FindAsync(id);
            _dbContext.Vehicles.Remove(result!);
        }

        public async Task<List<Vehicle>> GetAll()
        {
            return await _dbContext.Vehicles
                .Include(v => v.Category)
                .AsNoTracking()
                .ToListAsync();
        }
        async Task<Vehicle?> IVehicleReadOnlyRepository.GetById(long id)
        {
            return await _dbContext.Vehicles
                .Include(v => v.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);
        }
        async Task<Vehicle?> IVehicleUpdateOnlyRepository.GetById(long id)
        {
            return await _dbContext.Vehicles 
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public void Update(Vehicle vehicle)
        {
            _dbContext.Vehicles.Update(vehicle);
        }
    }
}
