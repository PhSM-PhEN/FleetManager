using System;
using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToClient;

public interface IClientReadOnlyRepository
{   
    Task<List<Client>> GetAll();
    Task<Client?> GetById(long id);
}
