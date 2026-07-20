using Bogus;
using Bogus.Extensions.Brazil;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Entities.ValueObjects;

namespace CommonTestUtilities.Entities
{
    public class TenantBuilder 
    {
        public static List<Tenant> Collection(uint count = 3)
        {
            var list = new List<Tenant>();
            if (count == 0)
                count = 1;
            var tenantId = 1;

            for (int i = 0; i < count; i++)
            {
                var tenant = Build();
                tenant.Id = tenantId++;
                list.Add(tenant);
            }
            return list;
        }

        public static Tenant Build(int? id = null, long? addressId = null)
        {
            var tenant = new Faker<Tenant>()
                .CustomInstantiator(f => new Tenant(
                    f.Person.FullName,
                    new Cpf(f.Person.Cpf()),
                    f.Random.Replace("##.###.###-#"),
                    new DriverLicense(f.Random.Replace("###########"), f.PickRandom("A", "B", "AB", "C", "D", "E")),
                    new Contact(f.Phone.PhoneNumber("(##) #####-####"), f.Internet.Email()),
                    addressId ?? f.Random.Long(1, 1000)
                ))
                .Generate();

            if (id.HasValue)
                tenant.Id = id.Value;

            return tenant;
        }
    }
}
