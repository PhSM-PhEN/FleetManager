using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToMaintenance;
using Moq;

namespace CommonTestUtilities.Repositories.ToMaintenance
{
    public class MaintenanceReadOnlyRepositoryBuilder
    {
        private readonly Mock<IMaintenanceReadOnlyRepository> _repository;
        public MaintenanceReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IMaintenanceReadOnlyRepository>();
        }

        public MaintenanceReadOnlyRepositoryBuilder GetById(long id, Maintenance? maintenance)
        {
            _repository.Setup(r => r.GetById(id)).ReturnsAsync(maintenance);
            return this;
        }

        public MaintenanceReadOnlyRepositoryBuilder GetAll(List<Maintenance> maintenances, int pageNumber, int pageSize, int totalCount)
        {
            _repository.Setup(r => r.GetAll(pageNumber, pageSize)).ReturnsAsync((maintenances, totalCount));
            return this;
        }

        public IMaintenanceReadOnlyRepository Build() => _repository.Object;
    }
}
