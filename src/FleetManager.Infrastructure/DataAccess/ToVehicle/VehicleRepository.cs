using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToVehicle;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToVehicle
{
    internal class VehicleRepository(FleetManagerDbContext dbContext) : IVehicleWriteOnlyRepository, IVehicleReadOnlyRepository, IVehicleUpdateOnlyRepository
    {
     
        public async Task Add(Vehicle vehicle)
        {
            await dbContext.Vehicles.AddAsync(vehicle);
        }

        public async Task Delete(long id)
        {
            var result =  await dbContext.Vehicles.FindAsync(id);
            dbContext.Vehicles.Remove(result!);
        }

        public async Task<List<Vehicle>> GetAll()
        {
            return await dbContext.Vehicles
                .AsNoTracking()
                .ToListAsync();
        }
        async Task<Vehicle?> IVehicleReadOnlyRepository.GetById(long id)
        {
            return await dbContext.Vehicles
                .AsNoTracking()
                .Include(v => v.Category)
                .FirstOrDefaultAsync(v => v.Id == id);
        }
        async Task<Vehicle?> IVehicleUpdateOnlyRepository.GetById(long id)
        {
            return await dbContext.Vehicles 
                .FirstOrDefaultAsync(v => v.Id == id);
        }
        public void Update(Vehicle vehicle)
        {
            dbContext.Vehicles.Update(vehicle);
        }
    }
}
