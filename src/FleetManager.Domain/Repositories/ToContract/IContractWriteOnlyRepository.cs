using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToContract
{
    public interface IContractWriteOnlyRepository
    {
        Task Add(Contract contract);
        Task<Contract?> GetById(long id);
        Task Delete(long id);
        void Update(Contract contract);
    }
}
