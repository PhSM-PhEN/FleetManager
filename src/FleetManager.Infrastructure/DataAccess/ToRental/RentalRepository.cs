using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToRental;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToRental;

public class RentalRepository(FleetManagerDbContext dbContext) : IRentalWriteOnlyRepository, IRentalReadOnlyRepository, IRentalUpdateOnlyRepository
{
   
    public async Task Add(Rental rental)
    {
        await dbContext.Rentals.AddAsync(rental);
    }

    public async Task Delete(long id)
    {
        var result = await dbContext.Rentals.FindAsync(id);
        dbContext.Rentals.Remove(result!);
    }

    public async Task<List<Rental>> GetAll()
    {
        return await dbContext.Rentals
        .Include(r => r.Client)
            .ThenInclude(c => c.Address)
        .Include(r => r.Vehicle)
        .Include(r =>r.Company)
            .ThenInclude(r => r.Address)
        .AsNoTracking().ToListAsync();
    }

    public async Task<Rental?> GetById(long id)
    {
        return await dbContext.Rentals.AsNoTracking()
        .Include(r => r.Company)
        .Include(r => r.Vehicle)
        .Include(r => r.Client)
        .FirstOrDefaultAsync(rent => rent.Id == id);
    }
    async Task<Rental?> IRentalUpdateOnlyRepository.GetById(int id)
    {
        return await dbContext.Rentals.FirstOrDefaultAsync(rent => rent.Id == id);
    }

    public void Update(Rental rental)
    {
        dbContext.Rentals.Update(rental);
    }
}
