using Bogus;
using FleetManager.Communication.Request.ToUser;

namespace CommonTestUtilities.Request.ToUser
{
    public class RequestLoginUserJsonBuilder
    {
        public static RequestLoginUserJson Build()
        {
            return new Faker<RequestLoginUserJson>()
                .RuleFor(request => request.Email, faker => faker.Internet.Email())
                .RuleFor(request => request.Password, faker => faker.Internet.Password(prefix: "aA1"))
                .Generate();
        }
    }
}