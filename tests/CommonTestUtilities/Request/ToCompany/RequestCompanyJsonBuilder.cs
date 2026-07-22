using Bogus;
using Bogus.Extensions.Brazil;
using FleetManager.Communication.Request.ToCompany;

namespace CommonTestUtilities.Request.ToCompany
{
    public class RequestCompanyJsonBuilder
    {
        public static RequestCompanyJson Build(long addressId)
        {
            return new Faker<RequestCompanyJson>()
                .RuleFor(request => request.Name, faker => faker.Company.CompanyName())
                .RuleFor(request => request.Cnpj, faker => faker.Company.Cnpj())
                .RuleFor(request => request.PhoneNumber, faker => faker.Phone.PhoneNumber("(##) #####-####"))
                .RuleFor(request => request.AddressId, _ => addressId)
                .Generate();
        }
    }
}