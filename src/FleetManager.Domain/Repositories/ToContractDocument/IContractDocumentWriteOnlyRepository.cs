using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToContractDocument
{
    public interface IContractDocumentWriteOnlyRepository
    {
        Task Add(ContractDocument document);
        Task<ContractDocument?> GetByContractId(long contractId);
    }
}
