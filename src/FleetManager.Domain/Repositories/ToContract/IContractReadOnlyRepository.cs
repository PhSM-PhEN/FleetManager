

using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToContract
{
    public interface IContractReadOnlyRepository
    {
        Task<Contract?> GetById(long id);
        Task<(List<Contract>, int TotalCount)> GetAll(int pageNumber, int pageSize);
        Task<bool> HasActiveContract(long vehicleId);

    }
}
