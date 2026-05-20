using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToVehicle
{
    public interface IVehicleReadOnlyRepository
    {
        Task<List<Vehicle>> GetAll();

        Task<Vehicle?> GetById(long id);
    }
}
