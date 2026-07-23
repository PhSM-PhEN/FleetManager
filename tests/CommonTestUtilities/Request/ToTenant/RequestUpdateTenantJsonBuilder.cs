using Bogus;
using FleetManager.Communication.Request.ToTenant;

namespace CommonTestUtilities.Request.ToTenant
{
    public class RequestUpdateTenantJsonBuilder
    {
        public static RequestUpdateTenantJson Build(long addressId)
        {
            return new Faker<RequestUpdateTenantJson>()
                .RuleFor(request => request.PhoneNumber, faker => faker.Phone.PhoneNumber("(##) #####-####"))
                .RuleFor(request => request.Email, faker => faker.Internet.Email())
                .RuleFor(request => request.AddressId, _ => addressId)
                .Generate();
        }
    }
}