using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToTenant;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class TenantReadOnlyRepositoryBuilder
    {
        private readonly Mock<ITenanteReadOnlyRepository> _repository;
        public TenantReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<ITenanteReadOnlyRepository>();
        }
        public TenantReadOnlyRepositoryBuilder GetAll(List<Tenant> tenants, int totalCount, int pageNumber, int pageSize)
        {
             _repository.Setup(t => t.GetAll(pageNumber, pageSize)).ReturnsAsync((tenants, totalCount));
             return this;
        }
          public TenantReadOnlyRepositoryBuilder ExistByCpf(string cpf, bool exists = false)
        {
            _repository.Setup(t => t.ExistByCpf(cpf)).ReturnsAsync(exists);
            return this;
        }
        public TenantReadOnlyRepositoryBuilder GetById(Tenant? tenant, long id)
        {
            _repository.Setup(t => t.GetById(id)).ReturnsAsync(tenant);
            return this;
        }
        public ITenanteReadOnlyRepository Build() => _repository.Object;
    }
}
