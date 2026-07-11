using Bogus;
using FleetManager.Communication.Request.ToUser;

namespace CommonTestUtilities.Request.ToUser
{
    public class RequestChangPasswordJsonBuilder
    {

        public static RequestChangPasswordJson Build()
        {
            return new Faker<RequestChangPasswordJson>()
                .RuleFor(user => user.OldPassword, faker => faker.Internet.Password(prefix: "aA1"))
                .RuleFor(user => user.NewPassword, faker => faker.Internet.Password(prefix: "aA1"));
                
        }

    }
}
