using System;
using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToRental;

public interface IRentalUpdateOnlyRepository
{
    Task<Rental?> GetById(long id);
    void Update(Rental rental);
}
