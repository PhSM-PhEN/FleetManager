using FleetManager.Domain.Repositories.ToAddress;
using Moq;

namespace CommonTestUtilities.Repositories;

public class AddressWriteOnlyRepositoryBuilder
{
    public IAddressWriteOnlyRepository Build()
    {
        var mock = new Mock<IAddressWriteOnlyRepository>();
        return mock.Object;
    }
}
