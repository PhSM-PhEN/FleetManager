using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.EnumExtensions
{
    public static class IncidentRiskExtensions
    {
        public static string IncidentRiskToString(this IncidentRisk risk)
        {
            return risk switch
            {
                IncidentRisk.Low => ResourceExtensionsMessages.LOW,
                IncidentRisk.High => ResourceExtensionsMessages.HIGH,
                _ => string.Empty
            };
        }
    }
}
