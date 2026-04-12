using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToVehicle
{
    public interface IVehicleUpdateOnlyRepository
    {
        void Update(Vehicle vehicle);
        Task<Vehicle?> GetById(long id);
    }
}
