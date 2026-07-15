using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Exception.ExceptionBase;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToAddress
{

    internal class AddressRepository(FleetManagerDbContext dbContext) :
        IAddressWriteOnlyRepository, IAddressReadOnlyRepository
    {
        public async Task Add(Address address)
        {
            await dbContext.Addresses.AddAsync(address);
        }

        public async Task Delete(long id)
        {
            var address = await dbContext.Addresses.FindAsync(id);
            dbContext.Addresses.Remove(address!);
        }

        public async Task<(List<Address>, int totalCount)> GetAll(int pageNumber, int pageSize)
        {
            var query = dbContext.Addresses.AsNoTracking();
            var totalCount = await query.CountAsync();
            var addresses = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (addresses, totalCount);
        }


        public async Task<Address?> GetById(long id)
        {
            return await dbContext.Addresses
                .FirstOrDefaultAsync(a => a.Id == id);

        }


        async Task<Address?> IAddressReadOnlyRepository.GetById(long id)
        {
            return await dbContext.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

        }

        public void Update(Address address)
        {
            dbContext.Addresses.Update(address);
        }
    }
}
