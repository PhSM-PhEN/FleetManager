using Bogus;
using FleetManager.Communication.Request.ToUser;

namespace CommonTestUtilities.Request.ToUser
{
    public class RequestChangePasswordJsonBuilder
    {

        public static RequestChangePasswordJson Build()
        {
            return new Faker<RequestChangePasswordJson>()
                .RuleFor(user => user.OldPassword, faker => faker.Internet.Password(prefix: "aA1"))
                .RuleFor(user => user.NewPassword, faker => faker.Internet.Password(prefix: "aA1"));
                
        }

    }
}
