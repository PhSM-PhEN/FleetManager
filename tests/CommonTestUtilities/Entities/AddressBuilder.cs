using Bogus;
using FleetManager.Domain.Entities;

namespace CommonTestUtilities.Entities
{
    public class AddressBuilder
    {
        public static List<Address> Collection(uint count = 3)
        {
            var list = new List<Address>();
            if (count == 0)
                count = 1;
            var addressId = 1;

            for (int i = 0; i < count; i++)
            {
                var address = Build();
                address.Id = addressId++;
                list.Add(address);
            }
            return list;
        }

        public static Address Build(long? id = null)
        {
            var address = new Faker<Address>()
                .CustomInstantiator(f => new Address(
                    f.Address.StreetName(),
                    f.Address.BuildingNumber(),
                    f.Address.City(),
                    f.Address.StateAbbr(),
                    f.Random.Replace("##.###-###")
                ))
                .Generate();

            if (id.HasValue)
                address.Id = id.Value;

            return address;
        }
    }
}