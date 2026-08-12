using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToMaintenance
{
    public interface IMaintenanceReadOnlyRepository
    {
        Task<Maintenance?> GetById(long id);
        Task<(List<Maintenance>, int totalCount)> GetAll(int pagenumber, int pageSize);
    }
}
