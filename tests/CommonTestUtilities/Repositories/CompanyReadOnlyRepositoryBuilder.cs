using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToCompany;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class CompanyReadOnlyRepositoryBuilder
    {
        private readonly Mock<ICompanyReadOnlyRepository> _repository;
        public CompanyReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<ICompanyReadOnlyRepository>();      
        }

        public CompanyReadOnlyRepositoryBuilder GetAll(List<Company> companies)
        {
            _repository.Setup(r => r.GetAll()).ReturnsAsync(companies);
            return this;
        }
        public CompanyReadOnlyRepositoryBuilder GetById(Company company)
        {
            _repository.Setup(r => r.GetById(company.Id)).ReturnsAsync(company);
            return this;
        }

        public ICompanyReadOnlyRepository Build() => _repository.Object;
    }
}
