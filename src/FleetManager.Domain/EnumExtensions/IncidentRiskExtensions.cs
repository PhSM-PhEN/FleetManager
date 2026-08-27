using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.EnumExtensions
{
    public static class IncidentRiskExtensions
    {
        public static string ToStringStatus(this IncidentRisk status)
        {
            return status switch
            {
                IncidentRisk.Low => ResourceExtensionsMessages.LOW,
                IncidentRisk.High => ResourceExtensionsMessages.HIGH,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }
}
