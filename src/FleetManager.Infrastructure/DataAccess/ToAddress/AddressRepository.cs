using System;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToAddress;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToAddress;

public class AddressRepository(FleetManagerDbContext dbContext) : IAddressWriteOnlyRepository, IAddressReadOnlyRepository, IAddressUpdateOnlyRepository
{
    private readonly FleetManagerDbContext _dbContext = dbContext;
    public async Task Add(Address address)
    {
        await _dbContext.Addresses.AddAsync(address);
    }

    public async Task Delete(long id)
    {
        var result = await _dbContext.Addresses.FindAsync(id);
         _dbContext.Addresses.Remove(result!);
        throw new NotImplementedException();
    }

    public async Task<List<Address>> GetAll()
    {
       return await _dbContext.Addresses.AsNoTracking().ToListAsync();
    }

    public async Task<Address?> GetById(long id)
    {
        return await _dbContext.Addresses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Update(Address address)
    {
        _dbContext.Addresses.Update(address);
    }
    async Task<Address?> IAddressUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContext.Addresses.FirstOrDefaultAsync(x => x.Id == id);
    }
}
