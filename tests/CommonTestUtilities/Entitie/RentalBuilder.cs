using Bogus;
using FleetManager.Domain.Entities;

namespace CommonTestUtilities.Entitie;

public class RentalBuilder
{
    public static Rental Build(long companyId = 1, long clientId = 1,
                               long vehicleId = 1, long userId = 1)
    {
        var faker = new Faker();
        var startDate = faker.Date.Future(1);
        var endDate = startDate.AddDays(faker.Random.Int(1, 30));

        var rental = new Rental(companyId, clientId, vehicleId, userId, startDate, endDate);
        rental.AttachPlan(RentalPlanBuilder.Build());

        return rental;
    }
}