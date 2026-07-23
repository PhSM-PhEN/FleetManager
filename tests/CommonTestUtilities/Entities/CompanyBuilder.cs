using Bogus;
using Bogus.Extensions.Brazil;
using FleetManager.Domain.Entities;

namespace CommonTestUtilities.Entities
{
    public class CompanyBuilder
    {
        public static List<Company> Collection(uint count = 3)
        {
            var list = new List<Company>();
            if (count == 0)
                count = 1;
            var companyId = 1;

            for (int i = 0; i < count; i++)
            {
                var company = Build();
                company.Id = companyId++;
                list.Add(company);
            }
            return list;
        }

        public static Company Build(long? id = null, long? addressId = null)
        {
            var company = new Faker<Company>()
                .CustomInstantiator(f => new Company(
                    f.Company.CompanyName(),
                    f.Company.Cnpj(),
                    f.Phone.PhoneNumber("(##) #####-####"),
                    addressId ?? f.Random.Long(1, 1000)
                ))
                .Generate();

            if (id.HasValue)
                company.Id = id.Value;

            return company;
        }
    }
}