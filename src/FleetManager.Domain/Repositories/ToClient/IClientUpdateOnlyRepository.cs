using System;
using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToClient;

public interface IClientUpdateOnlyRepository
{
    void Update(Client client);
    Task <Client?> GetById(long id);
}
