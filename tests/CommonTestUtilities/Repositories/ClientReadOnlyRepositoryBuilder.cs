using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToClient;
using Moq;

namespace CommonTestUtilities.Repositories;

public class ClientReadOnlyRepositoryBuilder
{
    private readonly Mock<IClientReadOnlyRepository> _repositoy;

    public ClientReadOnlyRepositoryBuilder()
    {
        _repositoy = new Mock<IClientReadOnlyRepository>();
    }
    public ClientReadOnlyRepositoryBuilder GetById(Client client)
    {
        _repositoy.Setup(c => c.GetById(client.Id)).ReturnsAsync(client);
        return this;
    }
    public ClientReadOnlyRepositoryBuilder GetAll(List<Client> clients)
    {
        _repositoy.Setup(c => c.GetAll()).ReturnsAsync(clients);
        return this;
    }

    public IClientReadOnlyRepository Build() => _repositoy.Object;

}
