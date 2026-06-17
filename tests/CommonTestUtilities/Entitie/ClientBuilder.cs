using Bogus;
using FleetManager.Domain.Entities;

namespace CommonTestUtilities.Entitie;

public class ClientBuilder
{
    public static Client Build(long addressId)
    {
        var faker = new Faker();
        return new Client(
            faker.Name.FullName(),
            faker.Phone.PhoneNumber(),
            faker.Random.Replace("##.###.###-#"),
            faker.Random.Replace("###.###.###-##"),
            faker.Random.Replace("#########"),
            faker.PickRandom("A", "B", "C", "D", "E", "AB"),
            addressId);
    }
}