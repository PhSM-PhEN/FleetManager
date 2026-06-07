using FleetManager.Domain.Repositories.ToVehicle;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class VehicleWriteOnlyRepositoryBuilder
    {
        public static IVehicleWriteOnlyRepository Build()
        {
            var mock = new Mock<IVehicleWriteOnlyRepository>();

            return mock.Object;
        }

       

    }
}
