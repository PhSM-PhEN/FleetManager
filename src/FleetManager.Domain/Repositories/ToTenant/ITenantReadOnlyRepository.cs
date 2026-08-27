using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToTenant
{
    public interface ITenantReadOnlyRepository
    {
        Task<(List<Tenant>, int totalCount)> GetAll(int pageNumber, int pageSize);
        Task<Tenant?> GetById(long id);
        Task<bool> ExistByCpf(string cpf);

    }
}
