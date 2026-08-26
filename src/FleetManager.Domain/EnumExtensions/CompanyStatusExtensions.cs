using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.EnumExtensions
{
    public static class CompanyStatusExtensions
    {
        public static string ToStringStatus(this CompanyStatus status)
        {
            return status switch
            {
                CompanyStatus.Available => ResourceExtensionsMessages.AVAILABLE,
                CompanyStatus.Unavailable => ResourceExtensionsMessages.UNAVAILABLE,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }
}
