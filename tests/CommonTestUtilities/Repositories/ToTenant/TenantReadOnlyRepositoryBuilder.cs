using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToTenant;
using Moq;

namespace CommonTestUtilities.Repositories.ToTenant
{
    public class TenantReadOnlyRepositoryBuilder
    {
        private readonly Mock<ITenantReadOnlyRepository> _repository;
        public TenantReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<ITenantReadOnlyRepository>();
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
        public ITenantReadOnlyRepository Build() => _repository.Object;
    }
}
