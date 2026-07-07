using Bogus;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;

namespace CommonTestUtilities.Entities
{
    public class UserBuilder
    {
        public static User Build(string role = Roles.TEAM_MEMBER)
        {
            var faker = new Faker();
            var name = faker.Name.FullName();
            var email = faker.Internet.Email(name);
            var password = faker.Internet.Password(prefix: "aA1");

            var user = new User(name, email, password);
            if (role == Roles.ADMIN)
            {
                user.PromoteToAdmin();
            }
            return user;
        }
    }
}
