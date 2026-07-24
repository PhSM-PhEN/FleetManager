using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToVehicle;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToVehicle
{
    internal class VehicleRepository(FleetManagerDbContext dbContext) : IVehicleWriteOnlyRepository, IVehicleReadOnlyRepository
    {
        public async Task Add(Vehicle vehicle)
        {
            await dbContext.Vehicles.AddAsync(vehicle);
        }

        public async Task Delete(long id)
        {
            var vehicle = await GetById(id);
            dbContext.Remove(vehicle!);
        }

        public async Task<(List<Vehicle>, int totalCount)> GetAll(int pageNumber, int pageSize)
        {
            var query = dbContext.Vehicles.AsNoTracking();
            var totalCount = await query.CountAsync();
            var vehicle = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            return (vehicle, totalCount);

        }

        public async Task<Vehicle?> GetById(long id)
        {
            return await dbContext.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
        }

        async Task<Vehicle?> IVehicleReadOnlyRepository.GetById(long id)
        {
            return await dbContext.Vehicles.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
        }
        public void Update(Vehicle vehicle)
        {
            dbContext.Update(vehicle);
        }
    }
}
