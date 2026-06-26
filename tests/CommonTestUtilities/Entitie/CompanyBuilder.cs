using Bogus;
using FleetManager.Domain.Entities;

namespace CommonTestUtilities.Entitie
{

    public class CompanyBuilder
    {
        public static Company Build(long addressId = 1)
        { 
            var faker = new Faker();
            var company = new Company(
                faker.Company.CompanyName(),
                faker.Random.Replace("##.###.###/#####-##"),
                faker.Phone.PhoneNumber(),
                addressId
            )
            {
                Address = AddressBuilder.Build()
            };

            return company;
        }
        

        
    }
}