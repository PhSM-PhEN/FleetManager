using FleetManager.Communication.Response.ToIncidentReport;
using FleetManager.Communication.Response.ToVehicle;

namespace FleetManager.Communication.Response.ToMaintenance
{
    public class ResponseMaintenanceJson
    {
        public long Id { get; set; }
        public string ServiceCenter {get ; set ;} = string.Empty;
        public DateTime ScheduledAt { get; set; }
        public decimal? WorkshopBudget { get; set; }
        public string? ProblemDescription { get; set; } = string.Empty;
        public ResponseEnumStatusJson Status { get; set; } = new();
        public ResponseRegisterVehicleJson Vehicle { get; set; } = new();
        public ResponseShortIncidentReportJson? IncidentReport { get; set; } = new();
    }
}
