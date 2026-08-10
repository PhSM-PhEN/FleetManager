using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToCharge
{
    public interface IChargeReadOnlyRepository
    {
        Task<List<Charge>> GetByContractId(long contractId);
    }
}
