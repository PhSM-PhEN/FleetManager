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
                CompanyStatus.Active => ResourceExtensionsMessages.ACTIVE,
                CompanyStatus.Inactive => ResourceExtensionsMessages.INACTIVE,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }
}
