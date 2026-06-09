using FleetManager.Domain.Entities;

namespace WebApi.Tests.Resource;

public class RentalIdentityManager(Rental rental)
{
    private readonly Rental _rental = rental;

    public long GetById() => _rental.Id;
    public DateTime GetStartDate() => _rental.StartDate;
    public DateTime GetEndDate() => _rental.EndDate;
}
