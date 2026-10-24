using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.EnumExtensions
{
    public static class TenantStatusExtension
    {
        public static string ToStringStatus(this TenantStatus status)
        {
            return status switch
            {
               TenantStatus.Active => ResourceExtensionsMessages.ACTIVE,
               TenantStatus.Rented => ResourceExtensionsMessages.RENTED,
               TenantStatus.Delinquent => ResourceExtensionsMessages.DELINQUENT,
               TenantStatus.Inactive => ResourceExtensionsMessages.INACTIVE,
               _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
        

    }
}
