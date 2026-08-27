using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToContractTemplate
{
    public interface IContractTemplateWriteOnlyRepository
    {
        Task Add(ContractTemplate template);
        Task<ContractTemplate?> GetById(long id);
        Task<ContractTemplate?> GetActive();
        void Update(ContractTemplate template);
    }
}
