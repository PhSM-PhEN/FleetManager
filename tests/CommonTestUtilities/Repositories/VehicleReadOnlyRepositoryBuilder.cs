using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToVehicle;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class VehicleReadOnlyRepositoryBuilder
    {
        private readonly Mock<IVehicleReadOnlyRepository> _repository;
        public VehicleReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IVehicleReadOnlyRepository>();
        }
        public VehicleReadOnlyRepositoryBuilder GetAll(List<Vehicle> vehicle, int pageNumber, int pageSize, int totalCount)
        {
            _repository.Setup(a => a.GetAll(pageNumber, pageSize)).ReturnsAsync((vehicle, totalCount));
            return this;
        }
        public VehicleReadOnlyRepositoryBuilder GetById(long id, Vehicle vehicle)
        {
            _repository.Setup(v => v.GetById(id)).ReturnsAsync(vehicle);
            return this;
        }
    }
}
