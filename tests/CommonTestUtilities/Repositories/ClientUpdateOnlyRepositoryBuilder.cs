using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToClient;
using Moq;

namespace CommonTestUtilities.Repositories;

public class ClientUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IClientUpdateOnlyRepository> _repository;
    public ClientUpdateOnlyRepositoryBuilder()
    {
        _repository = new Mock<IClientUpdateOnlyRepository>();
    }
    public ClientUpdateOnlyRepositoryBuilder GetById(Client client)
    {
        _repository.Setup(c => c.GetById(client.Id)).ReturnsAsync(client);
        return this;
    }

    public IClientUpdateOnlyRepository Build() => _repository.Object;
}
