using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToClient;
using Moq;

namespace CommonTestUtilities.Repositories;

public class ClientReadOnlyRepositoryBuilder
{
    private readonly Mock<IClientReadOnlyRepository> _repository;

    public ClientReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IClientReadOnlyRepository>();
    }
    public ClientReadOnlyRepositoryBuilder GetById(Client client)
    {
        _repository.Setup(c => c.GetById(client.Id)).ReturnsAsync(client);
        return this;
    }
    public ClientReadOnlyRepositoryBuilder GetAll(List<Client> clients)
    {
        _repository.Setup(c => c.GetAll()).ReturnsAsync(clients);
        return this;
    }

    public IClientReadOnlyRepository Build() => _repository.Object;

}
