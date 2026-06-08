using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToCompany;
using Moq;

namespace CommonTestUtilities.Repositories;

public class CompanyUpdateOnlyRepositoryBuilder
{
    private readonly Mock<ICompanyUpdateOnlyRepository> _repository;
    public CompanyUpdateOnlyRepositoryBuilder()
    {
        _repository = new Mock<ICompanyUpdateOnlyRepository>();
    }
    public CompanyUpdateOnlyRepositoryBuilder GetById(Company company)
    {
        _repository.Setup(r => r.GetById(company.Id)).ReturnsAsync(company);
        return this;
    }
    public ICompanyUpdateOnlyRepository Build() => _repository.Object;
}
