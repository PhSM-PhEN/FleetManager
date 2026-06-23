using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToVehicle;
using Moq;

namespace CommonTestUtilities.Repositories;

public class VehicleReadOnlyRepositoryBuilder
{
    private readonly Mock<IVehicleReadOnlyRepository> _repository;

    public VehicleReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IVehicleReadOnlyRepository>();
    }

    public VehicleReadOnlyRepositoryBuilder GetAll(List<Vehicle> vehicles, int pageNumber = 1, int pageSize = 10)
    {
        _repository.Setup(r => r.GetAll(pageNumber, pageSize)).ReturnsAsync((vehicles, vehicles.Count));
       
        return this;
    }
    public VehicleReadOnlyRepositoryBuilder GetById(long id, Vehicle vehicle)
    {
        _repository.Setup(r => r.GetById(id)).ReturnsAsync(vehicle);

        return this;
    }
    public IVehicleReadOnlyRepository Build() => _repository.Object;
}
