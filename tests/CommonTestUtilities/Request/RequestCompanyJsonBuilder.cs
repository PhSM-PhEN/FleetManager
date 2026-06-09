using Bogus;
using FleetManager.Communication.Requests;

namespace CommonTestUtilities.Request;

public class RequestCompanyJsonBuilder
{
    public static RequestCompanyJson Build(long addressId)
    {
        return new Faker<RequestCompanyJson>()
            .RuleFor(r => r.Name,        f => f.Company.CompanyName())
            .RuleFor(r => r.Cnpj,        f => f.Random.Replace("##.###.###/####-##"))
            .RuleFor(r => r.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(r => r.AddressId,   _ => addressId)
            .Generate();
    }
}