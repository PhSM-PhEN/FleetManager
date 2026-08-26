using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.EnumExtensions
{
    public static class IncidentReportStatusExtensions
    {
        public static string ToStringStatus(this IncidentReportStatus status)
        {
            return status switch
            {
                IncidentReportStatus.Reported => ResourceExtensionsMessages.REPORTED,
                IncidentReportStatus.Resolved => ResourceExtensionsMessages.RESOLVED,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }
}
