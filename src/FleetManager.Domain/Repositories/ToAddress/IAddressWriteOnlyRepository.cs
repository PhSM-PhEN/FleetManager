using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToAddress
{
    public interface IAddressWriteOnlyRepository
    {
        Task Add(Address address);
        Task<Address?> GetById(long id);
        Task Delete(Address address);
        void Update(Address address);
    }
}
