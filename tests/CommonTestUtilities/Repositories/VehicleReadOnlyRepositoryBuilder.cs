using System.Threading.Tasks;
using Bogus.DataSets;
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

    public async Task<VehicleReadOnlyRepositoryBuilder> GetAll()
    {
        
       
        return this;
    }
    public IVehicleReadOnlyRepository Build() => _repository.Object;
}
