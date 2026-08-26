using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToContractTemplate
{
    public interface IContractTemplateReadOnlyRepository
    {
        Task<ContractTemplate?> GetActive();
        Task<ContractTemplate?> GetById(long id);
        Task<(List<ContractTemplate>, int TotalCount)> GetAll(int pageNumber, int pageSize);
    }
}