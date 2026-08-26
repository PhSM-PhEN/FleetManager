using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.Entities
{
    public class User
    {
        public long Id { get; internal set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public Guid UserIdentifier { get; private set; }
        public string Role { get; private set; } = Roles.TEAM_MEMBER;
        public UserStatus Status { get; private set; }

        protected User() {}
        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            ChangePassword(password);
            UserIdentifier = Guid.NewGuid();
            Status = UserStatus.Active;
        }
        internal User(long id, Guid identifier, string name, string role)
        {
            Id = id;
            UserIdentifier = identifier;
            Name = name;
            Role = role;
            Status = UserStatus.Active;
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
                throw new ErrorOnValidationException([ResourceErrorMessages.USER_ALREADY_ADMIN]);
            }
            Role = Roles.ADMIN;

        }


        public void DemoteToTeamMember() => Role = Roles.TEAM_MEMBER;

    }
}
