using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToVehicle;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class VehicleUpdateOnlyRepositoryBuilder
    {
        private readonly Mock<IVehicleUpdateOnlyRepository> _repository;

        public VehicleUpdateOnlyRepositoryBuilder()
        {
            _repository = new Mock<IVehicleUpdateOnlyRepository>();
        }


        public  VehicleUpdateOnlyRepositoryBuilder GetById(Vehicle vehicle)
        {
            if (vehicle is not null)
            {
                _repository.Setup(r => r.GetById(vehicle.Id)).ReturnsAsync(vehicle);
            }
            return this;
        }

        public IVehicleUpdateOnlyRepository Build() => _repository.Object;
    }
}
