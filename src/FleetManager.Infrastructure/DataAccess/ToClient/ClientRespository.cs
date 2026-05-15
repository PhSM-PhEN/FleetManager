using System;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToClient;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToClient;

public class ClientRespository(FleetManagerDbContext dbContext) : IClientWriteOnlyRepository, IClientReadOnlyRepository, IClientUpdateOnlyRepository
{
    private readonly FleetManagerDbContext _dbContext = dbContext;

    public async Task Add(Client client)
    {
        await _dbContext.Clients.AddAsync(client);
    }

    public Task Delete(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Client>> GetAll()
    {
        return await _dbContext.Clients.AsNoTracking()
                     .ToListAsync();
    }

    public async Task<Client?> GetById(long id)
    {
        return await _dbContext.Clients.AsNoTracking().Include(a => a.Address).FirstOrDefaultAsync(client => client.Id ==id);
    }
    async Task<Client?> IClientUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContext.Clients.FirstOrDefaultAsync(client => client.Id == id);
    }

    public void Update(Client client)
    {
        throw new NotImplementedException();
    }
}
