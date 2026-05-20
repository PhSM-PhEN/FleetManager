using System;
using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToRental;

public interface IRentalWriteOnlyRepository
{
    Task Add(Rental rental);
    Task Delete(long id);
}
