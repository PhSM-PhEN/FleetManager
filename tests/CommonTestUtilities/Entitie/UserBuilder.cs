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

            var user = new Faker<User>()
                .RuleFor(u => u.Id, _ => 1)
                .RuleFor(u => u.Name, f => f.Person.FirstName)
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(u => u.Password, (_, user) => passwordEncrypter.Encrypt(user.Password))
                .RuleFor(u => u.UserIdentifier, _ => Guid.NewGuid())
                .RuleFor(u => u.Role, _ => role);

            return user;
        }
    }
}
