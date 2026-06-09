using Bogus;
using FleetManager.Communication.Requests;

namespace CommonTestUtilities.Request;

public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build()
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(request => request.Name, faker => faker.Name.FullName())
            .RuleFor(request => request.Email, (faker, user) => faker.Internet.Email(user.Name))
            .RuleFor(request => request.Password , faker => faker.Internet.Password(prefix: "aA1"))
            .Generate();
            
        
    }
}
