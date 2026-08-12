using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToMaintenance
{
    public interface IMaintenanceWriteOnlyRepository
    {
        Task Add(Maintenance maintenance);
        Task<Maintenance?> GetById(long id);
        Task Delete(Maintenance maintenance);
        void Update(Maintenance maintenance);
    }
}
