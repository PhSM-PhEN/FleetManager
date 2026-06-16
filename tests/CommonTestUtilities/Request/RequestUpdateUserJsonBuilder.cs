using Bogus;
using FleetManager.Communication.Requests;

namespace CommonTestUtilities.Request
{
    public class RequestUpdateUserJsonBuilder 
    {
        public static RequestUpdateUserJson Build()
        {
            return new Faker<RequestUpdateUserJson>()
            .RuleFor(request => request.Name, faker => faker.Name.FullName())
            .RuleFor(request => request.Email, (faker, user) => faker.Internet.Email(user.Name))
            .Generate();
        }
            
    }
}


