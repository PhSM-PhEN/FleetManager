using System;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToAddress;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToAddress;

public class AddressRepository(FleetManagerDbContext dbContext) : IAddressWriteOnlyRepository, IAddressReadOnlyRepository, IAddressUpdateOnlyRepository
{
    
    public async Task Add(Address address)
    {
        await dbContext.Addresses.AddAsync(address);
    }

    public async Task Delete(long id)
    {
        var result = await dbContext.Addresses.FindAsync(id);
         dbContext.Addresses.Remove(result!);
        
    }

    public async Task<List<Address>> GetAll()
    {
       return await dbContext.Addresses.AsNoTracking().ToListAsync();
    }

    public async Task<Address?> GetById(long id)
    {
        return await dbContext.Addresses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
  
    public void Update(Address address)
    {
        dbContext.Addresses.Update(address);
    }
    async Task<Address?> IAddressUpdateOnlyRepository.GetById(long id)
    {
        return await dbContext.Addresses.FirstOrDefaultAsync(x => x.Id == id);
    }
}
