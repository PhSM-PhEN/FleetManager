using Bogus;
using FleetManager.Communication.Request.ToUser;

namespace CommonTestUtilities.Request.ToUser
{
    public class RequestUpdateUserJsonBuilder
    {
        public static   RequestUpdateUserJson Build()
        {
            return new Faker<RequestUpdateUserJson>()
                .RuleFor(user => user.Name, faker => faker.Person.FirstName)
                .RuleFor(user => user.Email, (f, u) => f.Internet.Email(u.Name));
        }
    }
}
