using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.EnumExtensions
{
    public static class MaintenanceStatusExtensions
    {
        public static string ToMaintenanceString(this MaintenanceStatus status)
        {
            return status switch
            {
                MaintenanceStatus.Scheduled => ResourceExtensionsMessages.SCHEDULED,
                MaintenanceStatus.Closed => ResourceExtensionsMessages.CLOSED,
                _ => string.Empty,
            };
        }
    }
}
