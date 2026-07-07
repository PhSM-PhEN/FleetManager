using FleetManager.Domain.Enum;

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
        internal User(long id, Guid identifier, string name, string role)
        {
            Id = id;
            UserIdentifier = identifier;
            Name = name;
            Role = role;
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
        public void PromoteToAdmin()
        {
            if (Role == Roles.ADMIN)
            {
                
            }
            Role = Roles.ADMIN;

        }


        public void DemoteToTeamMember() => Role = Roles.TEAM_MEMBER;

    }
}
