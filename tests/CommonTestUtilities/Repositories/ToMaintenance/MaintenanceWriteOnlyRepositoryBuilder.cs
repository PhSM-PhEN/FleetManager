using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToMaintenance;
using Moq;

namespace CommonTestUtilities.Repositories.ToMaintenance
{
    public class MaintenanceWriteOnlyRepositoryBuilder
    {
        private readonly Mock<IMaintenanceWriteOnlyRepository> _repository;
        public MaintenanceWriteOnlyRepositoryBuilder()
        {
            _repository = new Mock<IMaintenanceWriteOnlyRepository>();
        }

        public MaintenanceWriteOnlyRepositoryBuilder Add(Maintenance maintenance)
        {
            _repository.Setup(r => r.Add(maintenance)).Returns(Task.CompletedTask);
            return this;
        }

        public MaintenanceWriteOnlyRepositoryBuilder GetById(long id, Maintenance? maintenance)
        {
            _repository.Setup(r => r.GetById(id)).ReturnsAsync(maintenance);
            return this;
        }

        public MaintenanceWriteOnlyRepositoryBuilder Delete(Maintenance maintenance)
        {
            _repository.Setup(r => r.Delete(maintenance)).Returns(Task.CompletedTask);
            return this;
        }

        public MaintenanceWriteOnlyRepositoryBuilder Update(Maintenance maintenance)
        {
            _repository.Setup(r => r.Update(maintenance));
            return this;
        }

        public IMaintenanceWriteOnlyRepository Build() => _repository.Object;
    }
}
