using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToContractTemplate
{
    public interface IContractTemplateReadOnlyRepository
    {
        Task<ContractTemplate?> GetById(long id);

        // Lista todos os templates atualmente ativos (podem ser vários ao mesmo tempo), para exibição
        // por título/finalidade na hora de escolher qual usar ao gerar o documento de um contrato.
        Task<List<ContractTemplate>> GetAllActive();

        Task<(List<ContractTemplate>, int TotalCount)> GetAll(int pageNumber, int pageSize, bool? onlyActive = null);
    }
}
