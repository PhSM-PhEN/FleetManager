using System;
using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToClient;

public interface IClientReadOnlyRepository
{   
    Task<(List<Client>, int totalCount)> GetAll(int pageNumber, int pageSize);
    Task<Client?> GetById(long id);
}
