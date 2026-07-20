using Bogus;
using Bogus.Extensions.Brazil;
using FleetManager.Communication.Request.ToTenant;

namespace CommonTestUtilities.Request.ToTenant
{
    public class RequestTenantJsonBuilder
    {
        public static RequestTenantJson Build(long addressId)
        {
            return new Faker<RequestTenantJson>()
                .RuleFor(request => request.Name, faker => faker.Person.FullName)
                .RuleFor(request => request.Cpf, faker => faker.Person.Cpf())
                .RuleFor(request => request.Rg, faker => faker.Random.Replace("##.###.###-#"))
                .RuleFor(request => request.DriverLicenseNumber, faker => faker.Random.Replace("###########"))
                .RuleFor(request => request.DriverLicenseCategory, faker => faker.PickRandom("A", "B", "AB", "C", "D", "E"))
                .RuleFor(request => request.PhoneNumber, faker => faker.Phone.PhoneNumber("(##) #####-####"))
                .RuleFor(request => request.Email, faker => faker.Internet.Email())
                .RuleFor(request => request.AddressId, _ => addressId)
                .Generate();
        }
    }
}