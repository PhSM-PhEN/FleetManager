using System.Threading.Tasks;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToTenant;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class TenantWriteOnlyRepositoryBuilder
    {
        private readonly Mock<ITenantWriteOnlyRepository> _repository;
        public TenantWriteOnlyRepositoryBuilder()
        {
            _repository = new Mock<ITenantWriteOnlyRepository>();
        }

        public TenantWriteOnlyRepositoryBuilder Add(Tenant tenant)
        {
            _repository.Setup(t => t.Add(tenant)).Returns(Task.CompletedTask);
            return this;
        }
        public TenantWriteOnlyRepositoryBuilder Delete(long id)
        {
            _repository.Setup(t => t.Delete(id)).Returns(Task.CompletedTask);
            return this;
        }
        public TenantWriteOnlyRepositoryBuilder GetById(Tenant? tenant, long id)
        {
            _repository.Setup(t => t.GetById(id)).ReturnsAsync(tenant);
            return this;
        }
        public TenantWriteOnlyRepositoryBuilder Update(Tenant tenant)
        {
            _repository.Setup(t => t.Update(tenant));
            return this;
        }

        public ITenantWriteOnlyRepository Build() => _repository.Object;
    }
}
