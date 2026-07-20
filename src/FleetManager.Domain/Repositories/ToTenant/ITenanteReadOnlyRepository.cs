using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToTenant
{
    public interface ITenanteReadOnlyRepository
    {
        Task<(List<Tenant>, int totalCount)> GetAll(int pageNumber, int pageSize);
        Task<Tenant?> GetById(long id);

    }
}
