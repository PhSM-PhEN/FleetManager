using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToRental;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        .Include(r => r.Company)
        .AsNoTracking().ToListAsync();
    }

    public async Task<Rental?> GetById(long id)
    {
        return await dbContext.Rentals.AsNoTracking()
        .Include(r => r.Company)
            .ThenInclude(r => r.Address)
        .Include(r => r.Vehicle)
            .ThenInclude(r => r.Category)
        .Include(r => r.Client)
            .ThenInclude(r => r.Address)
        .Include(r => r.RentalPlan)
        .FirstOrDefaultAsync(rent => rent.Id == id);
    }
    async Task<Rental?> IRentalUpdateOnlyRepository.GetById(long id)
    {
        return await dbContext.Rentals.FirstOrDefaultAsync(rent => rent.Id == id);
    }

    public void Update(Rental rental)
    {
        dbContext.Rentals.Update(rental);
    }

    public async Task<List<Rental>> FilterByMonth(User user, DateTime date)
    {
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;

        int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

        var endDate = new DateTime(year: date.Year, month: date.Month, daysInMonth, 23, minute: 59, second: 59);

        return await dbContext.Rentals
            .AsNoTracking()
                .Where(r => r.UserId == user.Id && r.StartDate >= startDate && r.StartDate <= endDate)
                .Include(r => r.Company)
                    .ThenInclude(r => r.Address)
                .Include(r => r.Vehicle)
                    .ThenInclude(r => r.Category)
                .Include(r => r.Client)
                    .ThenInclude(r => r.Address)
                .Include(r => r.RentalPlan)
                    .OrderBy(r => r.StartDate)
                .ToListAsync();


    }

    public async Task<bool> VehicleHasActiveRental(long vehicleId)
    {
        return await dbContext.Rentals.AsNoTracking()
            .AnyAsync(r => r.VehicleId == vehicleId &&
            (r.Status == Domain.Enums.RentalStatus.Active || r.Status == Domain.Enums.RentalStatus.Overdue));
    }
}
