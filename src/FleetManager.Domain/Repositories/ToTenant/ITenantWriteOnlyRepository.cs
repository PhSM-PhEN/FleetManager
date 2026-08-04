using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToTenant
{
    public interface ITenantWriteOnlyRepository
    {
        Task Add(Tenant tenant);
        Task<Tenant?> GetById(long id);     
        Task Delete(Tenant tenant);
        void Update(Tenant tenant);

    }
}
