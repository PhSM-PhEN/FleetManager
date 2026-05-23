using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToRental;

public interface IRentalUpdateOnlyRepository
{
    Task<Rental?> GetById(int id);
    void Update(Rental rental);
}
