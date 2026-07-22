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
            _repository.Setup(c => c.GetAll()).ReturnsAsync(companies);
            return this;
        }

        public CompanyReadOnlyRepositoryBuilder GetById(Company? company, long id)
        {
            _repository.Setup(c => c.GetById(id)).ReturnsAsync(company);
            return this;
        }

        public ICompanyReadOnlyRepository Build() => _repository.Object;
    }
}