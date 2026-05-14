using System;
using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToClient;

public interface IClientWriteOnlyRepository
{
    Task Add(Client client) ;
    Task Delete(long id);
}
