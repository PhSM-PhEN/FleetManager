using Bogus;
using FleetManager.Domain.Entities;

namespace CommonTestUtilities.Entitie
{
    public class AddressBuilder
    {
        public static Address Build()
        {
            var faker = new Faker();
            return new Address(
                faker.Address.StreetName(),
                faker.Address.BuildingNumber(),
                faker.Address.City(),
                faker.Address.StateAbbr(),
                faker.Address.ZipCode());
        }
    }
}
