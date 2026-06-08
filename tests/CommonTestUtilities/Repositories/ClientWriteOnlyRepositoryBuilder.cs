using FleetManager.Domain.Repositories.ToClient;
using Moq;

namespace CommonTestUtilities.Repositories;

public class ClientWriteOnlyRepositoryBuilder
{
    public IClientWriteOnlyRepository Build()
    {
        var mock = new Mock<IClientWriteOnlyRepository>();
        return mock.Object;
    }
}
