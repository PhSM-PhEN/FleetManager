using System;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToAddress;

namespace FleetManager.Infrastructure.DataAccess.ToAddress;

public class AddressRepository(FleetManagerDbContext dbContext) : IAddressWriteOnlyRepository
{
    private readonly FleetManagerDbContext _dbContext = dbContext;
    public async Task Add(Address address)
    {
        await _dbContext.Addresses.AddAsync(address);
    }

    public Task Delete(long id)
    {
        throw new NotImplementedException();
    }
}
