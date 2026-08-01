using FleetManager.Domain.Entities;

namespace WebApi.Tests.Resource
{
    public class ContractIdentityManager(Contract contract)
    {
        public long GetById() => contract.Id;
    }
}
