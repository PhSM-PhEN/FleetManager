using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToVehicle;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class VehicleWriteOnlyRepositoryBuilder
    {
        private readonly Mock<IVehicleWriteOnlyRepository> _repository;
        public VehicleWriteOnlyRepositoryBuilder()
        {
            _repository = new Mock<IVehicleWriteOnlyRepository>();
        }
        public VehicleWriteOnlyRepositoryBuilder Add(Vehicle vehicle)
        {
            _repository.Setup(v => v.Add(vehicle)).Returns(Task.CompletedTask);
            return this;
        }
        public VehicleWriteOnlyRepositoryBuilder Delete(long id)
        {
            _repository.Setup(v => v.Delete(id)).Returns(Task.CompletedTask);
            return this;
        }
        public VehicleWriteOnlyRepositoryBuilder GetById(long id, Vehicle vehicle)
        {
            _repository.Setup(v => v.GetById(id)).ReturnsAsync(vehicle);
            return this;
        }
        public VehicleWriteOnlyRepositoryBuilder Update(Vehicle vehicle)
        {
            _repository.Setup(v => v.Update(vehicle));
            return this;
        }
        public IVehicleWriteOnlyRepository Build() => _repository.Object;

    }
}
