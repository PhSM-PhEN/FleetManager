using Bogus;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enums;

namespace CommonTestUtilities.Entitie;

public class CategoryBuilder
{
    public static Category Build()
    {
        var faker = new Faker();
        return new Category(
            faker.PickRandom("SUV", "Hatch", "Sedan", "Pickup", "Minivan"),
            faker.PickRandom<TransmissionType>());
    }
}   