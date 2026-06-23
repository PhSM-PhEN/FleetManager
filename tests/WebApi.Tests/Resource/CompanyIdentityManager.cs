using FleetManager.Domain.Entities;

namespace WebApi.Tests.Resource
{
    public class CompanyIdentityManager(Company company)
    {
        private readonly Company _company = company;
        public long GetById() => _company.Id;
    }
}
