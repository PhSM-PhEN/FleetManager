using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string _name { get; private set; } = string.Empty;
        public string _email { get; private set; } = string.Empty;
        public string _password { get; private set; } = string.Empty;
        public Guid _userIdentifier { get; private set; }
        public string Role { get; private set; } = Roles.TEAM_MEMBER;

        public User(string name, string email, string password)
        {
            _name = name;
            _email = email;
            _password = password;
            _userIdentifier = new Guid();
        }
        
    }
}
