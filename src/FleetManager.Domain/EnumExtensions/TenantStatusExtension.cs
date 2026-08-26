using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.EnumExtensions
{
    public static class TenantStatusExtension
    {
        public static string ToStringStatus(this TenantStatus status)
        {
            return status switch
            {
               TenantStatus.Available => ResourceExtensionsMessages.AVAILABLE,
               TenantStatus.Rented => ResourceExtensionsMessages.RENTED,
               TenantStatus.Delinquent => ResourceExtensionsMessages.DELINQUENT,
               TenantStatus.Deactivate => ResourceExtensionsMessages.DEACTIVATE,
               _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
        

    }
}
