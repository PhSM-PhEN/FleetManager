using FleetManager.Domain.Entities;

namespace WebApi.Tests.Resource
{
    public class AddressIdentityManager(Address address)
    {
        public long GetById() => address.Id;
    }
}
