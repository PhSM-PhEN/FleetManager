using FleetManager.Domain.Entities;

namespace WebApi.Tests.Resource
{
    public class TenantIdentityManager(Tenant tenant)
    {
        public long GetById() => tenant.Id;
    }
}
