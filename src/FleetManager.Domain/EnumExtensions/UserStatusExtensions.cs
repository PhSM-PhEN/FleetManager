using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.EnumExtensions
{
    public static class UserStatusExtensions
    {
        public static string ToStringStatus(this UserStatus status)
        {
            return status switch
            {
                UserStatus.Active => ResourceExtensionsMessages.ACTIVE,
                UserStatus.Inactive => ResourceExtensionsMessages.INACTIVE,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }
}
