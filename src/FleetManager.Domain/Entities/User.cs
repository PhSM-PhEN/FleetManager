using FleetManager.Domain.DomainExceptionBase;
using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public Guid UserIdentifier { get; private set; }
        public string Role { get; private set; } = Roles.TEAM_MEMBER;

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            ChangePassword(password);
            UserIdentifier = Guid.NewGuid();
        }
        public void Update(string name, string email)
        {
            Name = name;
            Email = email;

        }
        public void ChangePassword(string encryptedPassword)
        {
            Password = encryptedPassword;
        }
        public void PrometeToAdmin()
        {
            if (Role == Roles.ADMIN)
            {
                throw new DomainRuleException(ResourceMessages.USER_ALREADY_ADMIN);
            }
                Role = Roles.ADMIN;
           
        }
        public void DemoteToTeamMember() => Role = Roles.TEAM_MEMBER;
    }
}
