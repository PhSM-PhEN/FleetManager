using FleetManager.Communication.Response.ToIncidentReport;
using FleetManager.Communication.Response.ToVehicle;

namespace FleetManager.Communication.Response.ToMaintenance
{
    public class ResponseMaintenanceJson
    {
        public long Id { get; set; }
        public DateTime ScheduledAt { get; set; }
        public decimal? WorkshopBudget { get; set; }
        public string? ProblemDescription { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public ResponseRegisterVehicleJson Vehicle { get; set; } = new();
        public ResponseShortIncidentReportJson? IncidentReport { get; set; } = new();
    }
}
