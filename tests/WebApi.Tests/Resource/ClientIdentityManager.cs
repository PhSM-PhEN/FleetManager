using FleetManager.Domain.Entities;

namespace WebApi.Tests.Resource
{
    public class ClientIdentityManager(Client client)
    {
        public long GetById() => client.Id;
    }
}


