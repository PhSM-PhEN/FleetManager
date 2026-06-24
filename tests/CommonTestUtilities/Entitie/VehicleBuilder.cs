using Bogus;
using FleetManager.Domain.Entities;

namespace CommonTestUtilities.Entitie;

public class VehicleBuilder
{
    public static Vehicle Build(long categoryId = 1)
    {
        var faker = new Faker();
        return new Vehicle(
            faker.Vehicle.Manufacturer(),
            faker.Vehicle.Model(),
            faker.Date.Past(8).Year,
            faker.Random.Replace("###########"),
            faker.Vehicle.Vin(),
            faker.Random.Replace("???-####"),
            faker.Commerce.Color(),
            categoryId,
            faker.Random.Decimal(0, 30000));
    }
}