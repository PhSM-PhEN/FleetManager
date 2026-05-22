using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToRental;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToRental;

public class RentalRepository(FleetManagerDbContext dbContext) : IRentalWriteOnlyRepository, IRentalReadOnlyRepository, IRentalUpdateOnlyRepository
{
    private readonly FleetManagerDbContext _dbContext = dbContext;
    public async Task Add(Rental rental)
    {
        await _dbContext.Rentals.AddAsync(rental);
    }

    public async Task Delete(long id)
    {
        var result = await _dbContext.Rentals.FindAsync(id);
        _dbContext.Rentals.Remove(result!);
    }

    public async Task<List<Rental>> GetAll()
    {
        return await _dbContext.Rentals.AsNoTracking().ToListAsync();
    }

    public async Task<Rental?> GetById(long id)
    {
        return await _dbContext.Rentals.AsNoTracking().FirstOrDefaultAsync(rent => rent.Id == id);
    }
    async Task<Rental?> IRentalUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContext.Rentals.FirstOrDefaultAsync(rent => rent.Id == id);
    }

    public void Update(Rental rental)
    {
        _dbContext.Rentals.Update(rental);
    }
}
