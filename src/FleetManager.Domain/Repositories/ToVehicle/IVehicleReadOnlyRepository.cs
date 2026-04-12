using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToVehicle
{
    public interface IVehicleReadOnlyRepository
    {
        public Task<List<Vehicle>> GetAll();

        public Task<Vehicle?> GetById(long id);
    }
}
