using Bogus;
using FleetManager.Communication.Request.ToUser;

namespace CommonTestUtilities.Request.ToUser
{
    public class RequestRegisterUserJsonBuilder
    {
        public static RequestRegisterUserJson Build()
        {
            return new Faker<RequestRegisterUserJson>()
                .RuleFor(x => x.Name, f => f.Person.FullName)
                .RuleFor(x => x.Email, (f, user) => f.Internet.Email(user.Name))
                .RuleFor(x => x.Password, f => f.Internet.Password(prefix: "aA1"));

        }
    }
}
