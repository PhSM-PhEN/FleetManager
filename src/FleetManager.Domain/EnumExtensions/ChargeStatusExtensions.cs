using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.EnumExtensions
{
    public static class ChargeStatusExtensions
    {
        public static string ToStringStatus(this ChargeStatus status)
        {
            return status switch
            {
                ChargeStatus.Pending => ResourceExtensionsMessages.PENDING,
                ChargeStatus.Paid => ResourceExtensionsMessages.PAID,
                ChargeStatus.Overdue => ResourceExtensionsMessages.OVERDUE,
                ChargeStatus.Cancelled => ResourceExtensionsMessages.CANCELLED,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }
}
