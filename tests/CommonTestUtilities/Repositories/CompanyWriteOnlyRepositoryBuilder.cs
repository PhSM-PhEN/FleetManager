using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToCompany;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class CompanyWriteOnlyRepositoryBuilder
    {
        private readonly Mock<ICompanyWriteOnlyRepository> _repository;
        public CompanyWriteOnlyRepositoryBuilder()
        {
            _repository = new Mock<ICompanyWriteOnlyRepository>();
        }

        public CompanyWriteOnlyRepositoryBuilder Add(Company company)
        {
            _repository.Setup(c => c.Add(company)).Returns(Task.CompletedTask);
            return this;
        }

        public CompanyWriteOnlyRepositoryBuilder Delete(long id)
        {
            _repository.Setup(c => c.Delete(id)).Returns(Task.CompletedTask);
            return this;
        }

        public CompanyWriteOnlyRepositoryBuilder GetById(Company? company, long id)
        {
            _repository.Setup(c => c.GetById(id)).ReturnsAsync(company);
            return this;
        }

        public CompanyWriteOnlyRepositoryBuilder Update(Company company)
        {
            _repository.Setup(c => c.Update(company));
            return this;
        }

        public ICompanyWriteOnlyRepository Build() => _repository.Object;
    }
}