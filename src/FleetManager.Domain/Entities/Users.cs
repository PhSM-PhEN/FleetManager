using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities
{
    public class Users
    {
        public long UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Guid UserIdentifier { get; set; }
        public string Role { get; set; } = Roles.TEAM_MEMBER;
    }
}
