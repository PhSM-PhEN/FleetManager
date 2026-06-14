using Bogus;
using CommonTestUtilities.Criptography;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enums;

namespace CommonTestUtilities.Entitie
{
    public class UserBuilder
    {
        public static User Build(string role = Roles.TEAM_MEMBER)
        {
            var passwordEncrypter = new PasswordEncrypterBuilder().Build();

            var faker = new Faker();
            var name = faker.Person.FirstName;
            var email = faker.Internet.Email(name);
            var password = passwordEncrypter.Encrypt(faker.Internet.Password(prefix: "aA1"));

            var user = new User(name, email, password);

            if (role == Roles.ADMIN)
                user.PromoteToAdmin();

            return user;
        }
    }
}
