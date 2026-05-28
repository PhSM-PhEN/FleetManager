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
        .Include(r => r.Vehicle)
        .Include(r =>r.Company)
        .AsNoTracking().ToListAsync();
    }

    public async Task<Rental?> GetById(long id)
    {
        return await dbContext.Rentals.AsNoTracking().FirstOrDefaultAsync(rent => rent.Id == id);
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
