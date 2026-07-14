using Bogus;
using FleetManager.Communication.Request.ToAddress;

namespace CommonTestUtilities.Request.ToAddress
{
    public class RequestAddressJsonBuilder
    {
    public static RequestAddressJson Build()
    {
        return new Faker<RequestAddressJson>()
            .RuleFor(request => request.Street,  faker => faker.Address.StreetName())
            .RuleFor(request => request.Number,  faker => faker.Address.BuildingNumber())
            .RuleFor(request => request.City,    faker => faker.Address.City())
            .RuleFor(request => request.State,   faker => faker.Address.StateAbbr())
            .RuleFor(request => request.ZipCode, faker => faker.Address.ZipCode())
            .Generate();
    }        
    }
}
