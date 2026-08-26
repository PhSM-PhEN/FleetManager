using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.EnumExtensions
{
    public static class ContractStatusExtensions
    {
        public static string ToStringStatus(this ContractStatus status)
        {
            return status switch
            {
                ContractStatus.Reserved => ResourceExtensionsMessages.RESERVED,
                ContractStatus.Active => ResourceExtensionsMessages.ACTIVE,
                ContractStatus.Cancelled => ResourceExtensionsMessages.CANCELLED,
                ContractStatus.Finished => ResourceExtensionsMessages.FINISHED,
                ContractStatus.Overdue => ResourceExtensionsMessages.OVERDUE,
                ContractStatus.Renewed => ResourceExtensionsMessages.RENEWED,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }
}
