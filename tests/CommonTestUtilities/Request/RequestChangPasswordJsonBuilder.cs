using Bogus;
using FleetManager.Communication.Requests;

namespace CommonTestUtilities.Request
{
    public class RequestChangPasswordJsonBuilder
    {
        public static RequestChangePasswordJson Build()
        {
            return new Faker<RequestChangePasswordJson>()
                .RuleFor(user => user.Password, faker => faker.Internet.Password())
                .RuleFor(user => user.NewPassword, faker => faker.Internet.Password(prefix: "!Aa1"));
        }
    }
}
