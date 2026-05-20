using System;
using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToRental;

public interface IRentalReadOnlyRepository
{
    Task<List<Rental>> GetAll();
    Task<Rental?> GetById(long id);
}
