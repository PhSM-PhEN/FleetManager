using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToRental;

public interface IRentalReadOnlyRepository
{
    Task<(List<Rental>, int totalCount)> GetAll(int pageNumber, int pageSize);
    Task<List<Rental>> FilterByMonth(User user, DateTime date);
    Task<bool> VehicleHasActiveRental(long vehicleId);
    Task<Rental?> GetById(long id);

}
