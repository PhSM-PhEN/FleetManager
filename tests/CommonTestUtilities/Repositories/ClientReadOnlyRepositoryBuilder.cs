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
    public ClientReadOnlyRepositoryBuilder GetAll(List<Client> clients, int pageNumber = 1, int pageSize = 10)
    {
        _repository.Setup(c => c.GetAll(pageNumber, pageSize)).ReturnsAsync((clients, clients.Count));
        return this;
    }

    public IClientReadOnlyRepository Build() => _repository.Object;

}
