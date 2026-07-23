using FleetManager.Domain.Entities;

namespace WebApi.Tests.Resource
{
    public class CompanyIdentityManager(Company company)
    {
        public long GetById() => company.Id;
    }
}