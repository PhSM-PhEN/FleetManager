using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToRental;

public interface IRentalReadOnlyRepository
{
    Task<List<Rental>> GetAll();
    Task<List<Rental>> FilterByMonth(User user, DateTime date);
    Task<bool> VehicleHasActiveRental(long vehicleId);
    Task<Rental?> GetById(long id);

}
