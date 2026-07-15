using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToAddress;

public interface IAddressReadOnlyRepository
{
    Task<(List<Address>, int totalCount)> GetAll(int pageNumber, int pageSize);
    Task<Address?> GetById(long id);
}
