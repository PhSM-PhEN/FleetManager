using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Exception.ExceptionBase;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToAddress
{
    public class AddressRepository(FleetManagerDbContext dbContext) : IAddressWriteOnlyRepository
    {
        public async Task Add(Address address)
        {
            await dbContext.Addresses.AddAsync(address);
        }

        public async Task Delete(long id)
        {
            var address = await GetById(id) ??
                throw new NotFoundException("Address not Found");
             dbContext.Addresses.Remove(address);
        }

        public async Task<Address> GetById(long id)
        {
            return await dbContext.Addresses.FirstOrDefaultAsync(a => a.Id == id) ??
                            throw new NotFoundException("address notfound");
        }

        public void Update(Address address)
        {
            dbContext.Addresses.Update(address);
        }
    }
}
