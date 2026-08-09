using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.EnumExtensions
{
    public static class IncidentReportStatusExtensions
    {
        public static string IncidentReportStatusToString(this IncidentReportStatus status)
        {
            return status switch
            {
                IncidentReportStatus.Reported => ResourceExtensionsMessages.REPORTED,
                IncidentReportStatus.Resolved => ResourceExtensionsMessages.RESOLVED,

                _ => string.Empty
            };
        }
    }
}
