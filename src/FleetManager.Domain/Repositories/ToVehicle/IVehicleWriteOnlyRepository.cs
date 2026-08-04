using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToVehicle
{
    public interface IVehicleWriteOnlyRepository
    {
        Task Add(Vehicle vehicle);
        Task Delete(Vehicle vehicle);
        Task<Vehicle?> GetById(long id);
        void Update(Vehicle vehicle);
    }
}
