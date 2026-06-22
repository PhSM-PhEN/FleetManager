using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToVehicle
{
    public interface IVehicleReadOnlyRepository
    {
        Task<(List<Vehicle>, int totalCount)> GetAll(int pageNumber, int pageSize);

        Task<Vehicle?> GetById(long id);
    }
}
